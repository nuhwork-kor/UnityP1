using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;

/// <summary>
/// 1. 플레이어가 총알 발사
/// 2. 총알 발사 최적화를 위한 오브젝트 풀링
/// </summary>
public class PlayerBulletFire : MonoBehaviour
{
    public GameObject bulletFactory;                //총알 공장 (프리팹)
    public Transform firePoint;                     //총알 발사 위치

    //Instantiate + Destroy 조합은 꽤 무거움. 오브젝트 풀링을 이용하는게 좋음
    // 오브젝트 풀링 = 미리 생성해둔 오브젝트들을 재사용하는 기법 ( 최적화 )
    // 장점 : 생성과 삭제에 따른(Instantiate + Destroy 조합으로인한) 성능 저하 감소
    // 단점 : 메모리 사용량 증가
    //오브젝트 풀링에 사용할 총알 최대 갯수
    int poolSize = 5;                              // 오브젝트 풀링에 사용할 총알 최대 갯수
    //int fireIndex = 0;                              // 다음에 발사할 총알 인덱스/ GameObject배열 만들때 사용

    //1. 배열로 오브젝트 풀링 구현
    //2. 리스트로 구현
    //3. 큐(Queue)로 구현       <<<
    //4. 스택(stack)으로 구현
    //오브젝트 풀링은 큐가 가장 성능이 좋다.

    // GameObject[] bulletPool;                        //총알 오브젝트 풀링 배열
    //public List<GameObject> listBulletPool;                //                  List 배열
    Queue<GameObject> queueBulletPool;              //                  Queue 배열

    private void Start()
    {
        InitObjectPooling();
    }

    void Update()
    {
        Fire();
    }

    /// <summary>
    /// 오브젝트 풀링 초기화
    /// </summary>
    void InitObjectPooling()
    {
        //// 1. GameObject 배열로 오브젝트 풀링 초기화
        //bulletPool = new GameObject[poolSize];
        //for (int i = 0; i < poolSize; i++)
        //{
        //    //총알 오브젝트 생성
        //    GameObject bullet = Instantiate(bulletFactory);
        //    //총알 오브젝트 비활성화
        //    bullet.SetActive(false);
        //    //배열에 저장
        //    bulletPool[i] = bullet;
        //}

        //2. List 배열로 오브젝트 풀링 초기화
        //listBulletPool = new List<GameObject>();
        //for(int i = 0; i < poolSize; i++)
        //{
        //    //총알 오브젝트 생성
        //    GameObject bullet = Instantiate(bulletFactory);
        //    //총알 오브젝트 비활성화
        //    bullet.SetActive(false);
        //    //리스트에 저장
        //    listBulletPool.Add(bullet);
        //}

        // 3. 큐(Queue)로 오브젝트 풀링 초기화
        queueBulletPool = new Queue<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            //총알 오브젝트 생성
            GameObject bullet = Instantiate(bulletFactory);
            //총알 오브젝트 비활성화
            bullet.SetActive(false);
            //큐에 저장
            queueBulletPool.Enqueue(bullet);            //큐는 인큐(Enqueue), 디큐(Dequeue)로 추가, 제거 함.
        }
    }

    /// <summary>
    /// 총알 실제 발사
    /// </summary>
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //총알 발사
            //Instantiate(bulletFactory, firePoint.position, firePoint.rotation);

            //총알 게임 오브젝트 생성 (이걸 줄인게 아래 있는 코드)
            //GameObject bullet = Instantiate(bulletFactory);
            //createBullet.transform.position = firePoint.position;
            //createBullet.transform.rotation = firePoint.rotation;

            //위치와 회전을 한번에 정해서 총알 생성
            //GameObject bullet = Instantiate(bulletFactory, firePoint.position, firePoint.rotation);
            GameObject bullet = Instantiate(bulletFactory, firePoint.position, Quaternion.identity);        
        }

        // 마우스 왼쪽클릭 or L CTRL
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //// 1. GameObject 배열로 오브젝트 풀링 발사
            //bulletPool[fireIndex].SetActive(true);
            //bulletPool[fireIndex].transform.position = firePoint.position;
            //bulletPool[fireIndex].transform.up = firePoint.up;                  //로테이션 대신 위쪽으로만 쏘게 설정
            //fireIndex++;
            //if(fireIndex >= poolSize)
            //{
            //    fireIndex = 0;
            //}

            // 2. List배열로 오브젝트 풀링 발사 
            // 2-1 위에꺼랑 똑같은 방법
            //listBulletPool[fireIndex].SetActive(true);
            //listBulletPool[fireIndex].transform.position = firePoint.position;
            //listBulletPool[fireIndex].transform.up = firePoint.up;
            //fireIndex++;
            //if(fireIndex >= poolSize)
            //{
            //    fireIndex = 0;
            //}
            //2-2 진짜 오브젝트 풀링
            //if (listBulletPool.Count > 0)
            //{
            //    //리스트에서 첫번째 오브젝트 가져오기
            //    GameObject bullet = listBulletPool[0];
            //    //오브젝트 활성화 및 위치, 회전 설정
            //    bullet.SetActive(true);
            //    bullet.transform.position = firePoint.position;
            //    bullet.transform.up = firePoint.up;
            //    //오브젝트 풀에서 빼준다
            //    listBulletPool.Remove(bullet);
            //}
            //else //오브젝트 풀이 비어있는 경우, 공속이 빨라서 오브젝트 풀 안에있는 총알 다 썼을때
            //{
            //    GameObject bullet = Instantiate(bulletFactory);
            //    bullet.SetActive(false);
            //    //오브젝트 풀에 추가
            //    listBulletPool.Add(bullet);
            //}

            //3. 큐(Queue)로 오브젝트 풀링 발사
            if(queueBulletPool.Count > 0)
            {
                Debug.Log("총알 발사");

                //큐에서 오브젝트 꺼내기
                GameObject bullet = queueBulletPool.Dequeue();
                //오브젝트 활성화 및 위치, 회전 설정
                bullet.SetActive(true);
                bullet.transform.position = firePoint.position;
                bullet.transform.up = firePoint.right;
            }
            else
            {
                Debug.Log("총알 모자라서 새로 만들었음");

                GameObject bullet = Instantiate(bulletFactory);
                bullet.SetActive(false);
                queueBulletPool.Enqueue(bullet);
            }

        }
    }

    /// <summary>
    /// 오브젝트 풀에 다시 추가해주는 함수. DestroyZone에서 호출할 거라 public으로 생성// 이거없으면 총알프리팹 계속 생성함
    /// </summary>
    /// <param name="obj"></param>
    public void ReloadPool(GameObject obj)
    {
        Debug.Log("총알 다시 주웠음");

        //총알 오브젝트 비활성화
        obj.SetActive(false);
        //큐에 다시 오브젝트 넣어주기
        queueBulletPool.Enqueue(obj);
    }
}
