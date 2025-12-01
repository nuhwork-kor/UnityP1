using UnityEngine;

/// <summary>
/// 1. 보스 등장
/// - 일정 시간이 지난 후 등장
/// - 에너미가 모두 제거된 후 등장
/// 2. 보스 총알 패턴
/// - 플레이어를 추적하는 총알 발사
/// - 회전 총알 발사
/// </summary>
public class Boss : MonoBehaviour
{
    public GameObject BossBulletFactory;                // 보스 총알 공장
    public Transform target;                            // 플레이어 위치
    public float fireTime = 1f;                         // 총알 발사 간격
    float timer = 0f;                                   // 타이머

    public int bulletMax = 10;                          //보스 최대 총알 갯수

    void Update()
    {
        AutoFire1();
    }

    void AutoFire1()
    {
        // 타깃이 없을 때 총알 발사 금지
        if( target != null)
        {
            timer += Time.deltaTime;
            if(timer >= fireTime)
            {
                timer = 0f;

                // 총알 회전하게 만들기
                for(int i = 0;  i < 10; i++)
                {
                    // 총알 공장에서 총알 생성
                    GameObject bullet = Instantiate(BossBulletFactory);
                    // 총알 위치를 보스 위치로 변경
                    bullet.transform.position = transform.position;
                    // 총알 방향을 타겟 방향으로 변경
                    Vector3 dir = target.position - transform.position;
                    dir.Normalize();

                    //회전 각도 계산
                    float angle = 360 / bulletMax;  //360도 / 10발 = 36도 간격
                    bullet.transform.eulerAngles = new Vector3(0,0,i*angle);

                    //총구의 방향도 맞춰준다 (이게 중요함)
                    bullet.transform.up = dir;
                }

            }
        }
    }
}
