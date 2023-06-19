using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletSpd = 10f;
    public float BulletSpd
    {
        get
        {
            return _bulletSpd;
        }
        set
        {
            _bulletSpd = value;
            if (_bulletSpd < 0)
            {
                _bulletSpd = 0f;
            }
        }
    }

    private float _angle = 0f;
    public float Angle
    {
        get
        {
            return _angle;
        }
        set
        {
            _angle = value;
        }
    }

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        _bulletSpd = 10f;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _bulletSpd * Time.deltaTime, Space.Self);
        if (_angle > 0f || _angle < 0f)
        {
            transform.Rotate(new Vector3(0, 0, _angle) * Time.deltaTime);
        }

        //angle += Time.deltaTime;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
        {
            Pool();
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    public void Pool()
    {
        BulletPool.Instance.Push(this);
    }

    public IEnumerator Acc(float accel = 0.2f, float limitSpeed = 10f)
    {
        while (_bulletSpd < limitSpeed)
        {
            _bulletSpd += accel;
            if (_bulletSpd > limitSpeed)
            {
                _bulletSpd = limitSpeed;
            }

            yield return waitForEndOfFrame;
        }
    }

}
