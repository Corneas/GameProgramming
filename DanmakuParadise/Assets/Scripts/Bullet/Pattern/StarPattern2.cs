using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPattern2 : ShootingBase
{
    // �Ѿ� ��ġ ����
    [SerializeField]
    private float interval = 0.1f;

    // �� ������
    [SerializeField]
    private float star1Size = 2f;
    [SerializeField]
    private float star2Size = 4f;

    // �� ����
    [SerializeField]
    private float star1Angle = 0f;
    [SerializeField]
    private float star2Angle = 180f;

    // �Ѿ� ��
    [SerializeField]
    private Color star1Color;
    [SerializeField]
    private Color star2Color;

    // �� ������ �Ѿ� �ӵ�
    [SerializeField]
    private float star1Speed = 7.5f;
    [SerializeField]
    private float star2Speed = 9f;

    // �Ѿ� ��ġ �ӵ�
    [Tooltip("�۾��� ���� ������")]
    [SerializeField]
    private float starPlaceSpeed = 0.025f;

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

    private WaitForSeconds waitStarPlaceSpeed;

    Coroutine co1;
    Coroutine co2;

    private void Awake()
    {
        // �ʱ� ��ġ �ʱ�ȭ
        initPos = transform.parent.position;
        waitStarPlaceSpeed = new WaitForSeconds(starPlaceSpeed);
    }

    protected override void StartPattern()
    {
        isEnd = false;
        StartCoroutine(MakeMainStar(star1Size, star1Angle, star1Color, star1Speed));
        StartCoroutine(MakeMainStar(star2Size, star2Angle, star2Color, star2Speed));
    }

    // �� ���� x��ǥ
    private float[] starX = { 0f, -0.59f, 0.95f, -0.95f, 0.59f, 0f };
    // �� ���� y��ǥ
    private float[] starY = { 1f, -0.81f, 0.31f, 0.31f, -0.81f, 1f };

    public IEnumerator MakeMainStar(float starSize, float starAngle, Color color, float bulletSpeed = 7.5f)
    {
        List<Bullet> bulletList = new List<Bullet>();

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

                Bullet bullet = BulletPool.Instance.Pop(transform.position);
                bullet.BulletSpd = 0f;
                bullet.SetBulletColor(color);

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
                bullet.transform.Rotate(0, 0, GetAngle(initPos, loc) + bulletAngle);

                bulletList.Add(bullet);

                yield return waitStarPlaceSpeed;

            }
            value = 0f;
        }

        yield return new WaitForSeconds(0.5f);

        ShootFreezingBullet(bulletList.ToArray(), bulletSpeed);
        bulletList.Clear();

        //StartCoroutine(End());
    }

    bool isEnd = false;
    // �ӽ�
    public IEnumerator End()
    {
        if (isEnd) yield break;
        isEnd = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    public Vector3 LerpTwoPoint(Vector3 P1, Vector3 P2, float value)
    {
        Vector3 a = Vector3.Lerp(P1, P2, value);

        return a;
    }

    // ���ü� ���� ���� �ʿ�
    public float GetAngle(Vector3 targetPos, Vector3 myPos)
    {
        Vector3 dir = targetPos - myPos;
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return rot;
    }

    public void ShootFreezingBullet(Bullet[] bulletArr, float speed = 7.5f)
    {
        for (int i = 0; i < bulletArr.Length; ++i)
        {
            bulletArr[i].BulletSpd = speed;
        }
    }
}
