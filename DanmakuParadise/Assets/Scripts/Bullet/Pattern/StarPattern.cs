using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPattern : ShootingBase
{
    public GameObject bulletPrefab = null;

    // 총알 배치 간격
    [SerializeField]
    private float interval = 0.1f;

    // 별 사이즈
    [SerializeField]
    private float starSize = 2f;

    // 별 각도
    [SerializeField]
    private float starAngle = 0f;

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

    private WaitForSeconds waitForZeroPointZeroFive = new WaitForSeconds(0.05f);

    private List<Bullet> bulletList = new List<Bullet>();

    private void Awake()
    {
        // 초기 위치 초기화
        initPos = transform.parent.position;
    }

    protected override void StartPattern()
    {
        StartCoroutine(Star());
    }

    #region 게임 매니악스 탄막 별 예제
    // 별 정점 x좌표
    private float[] starX = { 0f, -0.59f, 0.95f, -0.95f, 0.59f, 0f};
    // 별 정점 y좌표
    private float[] starY = { 1f, -0.81f, 0.31f, 0.31f, -0.81f, 1f};

    public IEnumerator Star()
    {
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

                //Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                Bullet bullet = BulletPool.Instance.Pop(transform.position);
                bullet.BulletSpd = 0f;

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
                bullet.transform.Rotate(0, 0, GetAngle(initPos, loc) /*+ bulletAngle*/);

                bulletList.Add(bullet);

                yield return waitForZeroPointZeroFive;
                
            }
            value = 0f;
        }

        yield return new WaitForSeconds(0.5f);

        ShootFreezingBullet(bulletList.ToArray());
        bulletList.Clear();
        // 임시
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
