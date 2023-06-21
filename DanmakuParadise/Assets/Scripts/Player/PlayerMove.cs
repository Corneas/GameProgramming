using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float h;
    float v;
    Vector3 dir;

    private float speed = 10f;

    private int damageCount = 0;

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        dir = new Vector3(h, v, 0).normalized;

        transform.Translate(dir * Time.deltaTime * speed);

        if(damageCount > 10)
        {
            Debug.Log("GameOver");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 5f;
        }
        else
        {
            speed = 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            damageCount++;
        }
    }
}
