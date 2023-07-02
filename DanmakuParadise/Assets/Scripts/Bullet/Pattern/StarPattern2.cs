using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPattern2 : ShootingBase
{
    // 총알 배치 간격
    [SerializeField]
    private float interval = 0.1f;

    // 별 사이즈
    [SerializeField]
    private float star1Size = 2f;
    [SerializeField]
    private float star2Size = 4f;

    // 별 각도
    [SerializeField]
    private float star1Angle = 0f;
    [SerializeField]
    private float star2Angle = 180f;

    // 총알 색
    [SerializeField]
    private Color star1Color;
    [SerializeField]
    private Color star2Color;

    // 별 퍼지는 총알 속도
    [SerializeField]
    private float star1Speed = 7.5f;
    [SerializeField]
    private float star2Speed = 9f;

    // 총알 배치 속도
    [Tooltip("작아질 수록 빨라짐")]
    [SerializeField]
    private float starPlaceSpeed = 0.025f;

    // 라디안값
    private float rad = 0f;
    // 코사인
    private float c = 0;
    // 사인
    private float s = 0;

    // 총알의 각도
    [SerializeField]
    private float bulletAngle = 0f;

    // 초기 위치
    private Vector3 initPos;

    private WaitForSeconds waitStarPlaceSpeed;

    Coroutine co1;
    Coroutine co2;

    private void Awake()
    {
        // 초기 위치 초기화
        initPos = transform.parent.position;
        waitStarPlaceSpeed = new WaitForSeconds(starPlaceSpeed);
    }

    protected override void StartPattern()
    {
        isEnd = false;
        StartCoroutine(MakeMainStar(star1Size, star1Angle, star1Color, star1Speed));
        StartCoroutine(MakeMainStar(star2Size, star2Angle, star2Color, star2Speed));
    }

    // 별 정점 x좌표
    private float[] starX = { 0f, -0.59f, 0.95f, -0.95f, 0.59f, 0f };
    // 별 정점 y좌표
    private float[] starY = { 1f, -0.81f, 0.31f, 0.31f, -0.81f, 1f };

    public IEnumerator MakeMainStar(float starSize, float starAngle, Color color, float bulletSpeed = 7.5f)
    {
        List<Bullet> bulletList = new List<Bullet>();

        // 보간 위치
        float value = 0f;
        // 선형보간 후 좌표
        Vector3 lerpPos = Vector3.zero;
        Vector3 _P1;
        Vector3 _P2;

        for (int i = 0; i < starX.Length - 1; ++i)
        {
            // 정점의 좌표를 가져온다
            _P1 = new Vector3(starX[i] * starSize, starY[i] * starSize);
            _P2 = new Vector3(starX[i + 1] * starSize, starY[i + 1] * starSize);
            // 한 줄당 10프레임
            while (value <= 1f)
            {
                value += interval;

                // 선형보간
                lerpPos = LerpTwoPoint(_P1, _P2, value);

                Bullet bullet = BulletPool.Instance.Pop(transform.position);
                bullet.BulletSpd = 0f;
                bullet.SetBulletColor(color);

                // 별의 각도를 라디안값으로 변경
                rad = starAngle * Mathf.Deg2Rad;
                c = Mathf.Cos(rad);
                s = Mathf.Sin(rad);

                // 행렬을 이용한 각도 회전
                // { cos -sin } { x }
                // { sin  cos } { y }
                // x = (초기위치) + (보간 후 x값 * 코사인값 - 보간 후 y값 * 사인값)
                // y = (초기위치) + (보간 후 x값 * 사인값 + 보간 후 y값 * 코사인값)
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
    // 임시
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

    // 동시성 접근 관리 필요
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
