using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float h;
    float v;
    Vector3 dir;

    private float speed = 10f;

    public int damageCount { private set; get; } = 0;

    private bool isDamaged = false;

    private SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        dir = new Vector3(h, v, 0).normalized;

        transform.Translate(dir * Time.deltaTime * speed);

        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);

        playerPos.x = Mathf.Clamp(playerPos.x, 0.02f, 0.98f);
        playerPos.y = Mathf.Clamp(playerPos.y, 0.03f, 0.97f);

        transform.position = Camera.main.ViewportToWorldPoint(playerPos);

        if (damageCount > 4)
        {
            UIManager.Instance.GameOver();
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
            StartCoroutine(Damaged());
        }
    }

    public IEnumerator Damaged()
    {
        if (isDamaged)
            yield break;

        isDamaged = true;

        ++damageCount;
        UIManager.Instance.UpdateHpUI(damageCount - 1);

        for(int i = 0; i < 3; ++i)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDamaged = false;
    }
}
