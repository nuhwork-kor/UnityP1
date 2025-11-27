using UnityEngine;

/// <summary>
/// 1. 총알 이동 (방향은 설정해줘야 한다)
/// 2. 화면 밖으로 나가면 삭제 ( 충돌, 거리 )
/// </summary>
public class Bullet : MonoBehaviour
{
    public float speed = 10f;           //초당 10유닛 이동
    public float destroyDistance = 20f; //발사 위치로부터 20 유닛 떨어지면 삭제
    Vector3 startPosition;              //발사 위치

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //발사 위치 저장
       startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 총알 이동
        //transform.position += transform.up * speed * Time.deltaTime;
        transform.Translate(Vector3.up * speed *  Time.deltaTime);


        // 2. 발사 위치롭터 일정 거리 이상 떨어지면 삭제
        // 2-1. 거리 계산
        // if(startPosition.y > destroyDistance)  Destroy(gameObject);

        // Vector3.Distance();                                                          (제곱근 계산이라 성능저하가 좀 있음)
        // Distance(a, b) : a와 b 사이의 거리 반환
        // 피타고라스의 정리 응용 => 두 지점간의 차이의 제곱근
        // float distance = Vector3.Distance(startPosition, transform.position);
        // if (distance > destroyDistance)
        // {
        //     Destroy(gameObject);
        // }

        //정교한 거리가 필요 없고 단순 비교만 할거면 !!! sqrMagnitude !!!를 쓰는게 좋다.
        // Magnitude 속성도 마찬가지로 제곱근 계산을 한다.                                  (Distance보다 조금 빠름)

        // sqrMagnitude 속성은 제곱근 계산을 하지 않기 때문에 성능에 유리하다.                (연산 제일 빠름/ 최적화) 성능 향상을 위해 sqrMagnitude를 사용했습니다
        // 제곱근 거리 비교 ( 성능 향상 )
        //float sqrDistance = (startPosition - transform.position).sqrMagnitude;
        //if (sqrDistance > destroyDistance * destroyDistance)
        //{
        //    Destroy(gameObject);
        //}     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
