using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPattern : ShootingBase
{
    public GameObject bulletPrefab = null;

    // �Ѿ� ��ġ ����
    [SerializeField]
    private float interval = 0.1f;

    // �� ������
    [SerializeField]
    private float starSize = 2f;

    // �� ����
    [SerializeField]
    private float starAngle = 0f;

    // ���Ȱ�
    private float rad = 0f;
    // �ڻ���
    private float c = 0;
    // ����
    private float s = 0;

    // �Ѿ��� ����
    [SerializeField]
    private float bulletAngle = 0f;

    // �ʱ� ��ġ
    private Vector3 initPos;

    private WaitForSeconds waitForZeroPointZeroFive = new WaitForSeconds(0.05f);

    private List<Bullet> bulletList = new List<Bullet>();

    private void Awake()
    {
        // �ʱ� ��ġ �ʱ�ȭ
        initPos = transform.parent.position;
    }

    protected override void StartPattern()
    {
        StartCoroutine(Star());
    }

    #region ���� �ŴϾǽ� ź�� �� ����
    // �� ���� x��ǥ
    private float[] starX = { 0f, -0.59f, 0.95f, -0.95f, 0.59f, 0f};
    // �� ���� y��ǥ
    private float[] starY = { 1f, -0.81f, 0.31f, 0.31f, -0.81f, 1f};

    public IEnumerator Star()
    {
        // ���� ��ġ
        float value = 0f;
        // �������� �� ��ǥ
        Vector3 lerpPos = Vector3.zero;
        Vector3 _P1;
        Vector3 _P2;

        for (int i = 0; i < starX.Length - 1; ++i)
        {
            // ������ ��ǥ�� �����´�
            _P1 = new Vector3(starX[i] * starSize, starY[i] * starSize);
            _P2 = new Vector3(starX[i + 1] * starSize, starY[i + 1] * starSize);
            // �� �ٴ� 10������
            while (value <= 1f)
            {
                value += interval;

                // ��������
                lerpPos = LerpTwoPoint(_P1, _P2, value);

                //Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                Bullet bullet = BulletPool.Instance.Pop(transform.position);
                bullet.BulletSpd = 0f;

                // ���� ������ ���Ȱ����� ����
                rad = starAngle * Mathf.Deg2Rad;
                c = Mathf.Cos(rad);
                s = Mathf.Sin(rad);

                // ����� �̿��� ���� ȸ��
                // { cos -sin } { x }
                // { sin  cos } { y }
                // x = (�ʱ���ġ) + (���� �� x�� * �ڻ��ΰ� - ���� �� y�� * ���ΰ�)
                // y = (�ʱ���ġ) + (���� �� x�� * ���ΰ� + ���� �� y�� * �ڻ��ΰ�)
                Vector3 loc = new Vector3(initPos.x + (lerpPos.x * c - lerpPos.y * s), initPos.y + (lerpPos.x * s + lerpPos.y * c));

                bullet.transform.position = loc;
                bullet.transform.Rotate(0, 0, GetAngle(initPos, loc) /*+ bulletAngle*/);

                bulletList.Add(bullet);

                yield return waitForZeroPointZeroFive;
                
            }
            value = 0f;
        }

        yield return new WaitForSeconds(0.5f);

        ShootFreezingBullet(bulletList.ToArray());
        bulletList.Clear();
        // �ӽ�
        //gameObject.SetActive(false);
    }

    public Vector3 LerpTwoPoint(Vector3 P1, Vector3 P2, float value)
    {
        Vector3 a = Vector3.Lerp(P1, P2, value);

        return a;
    }

    public float GetAngle(Vector3 targetPos, Vector3 myPos)
    {
        Vector3 dir = targetPos - myPos;
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Debug.Log(rot);

        return rot;
    }

    public void ShootFreezingBullet(Bullet[] bulletArr)
    {
        for(int i = 0; i < bulletArr.Length; ++i)
        {
            bulletArr[i].BulletSpd = 7.5f;
        }
    }

    #endregion
}
