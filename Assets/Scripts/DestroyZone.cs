using UnityEditor;
using UnityEngine;

    /// <summary>
    /// 충돌 감지 후 오브젝트 삭제 또는 비활성화
    /// </summary>
public class DestroyZone : MonoBehaviour
{

    public PlayerBulletFire pf;                                                                                   // 참조하는 방법 1
    public GameObject Player;                                                                                     // 참조하는 방법 1

    private void Start()
    {
        pf = Player.GetComponent<PlayerBulletFire>();                                                             // 참조하는 방법 1
    }

    private void OnTriggerEnter(Collider other)
    {
        ////충돌한 오브젝트의 태그가 Bullet일 경우
        //if (other.CompareTag("Bullet"))
        //{
        //    //오브젝트 비활성화
        //    other.gameObject.SetActive(false);
        //    // or 오브젝트 삭제
        //    //Destroy(other.gameObject);
        //}

        ////충돌한 오브젝트의 이름이 "Bullet"인 경우
        //if(other.name.Contains("Bullet"))
        //{
        //    //오브젝트 비활성화
        //    other.gameObject.SetActive(false);
        //    // or 오브젝트 삭제
        //    //Destroy(other.gameObject);
        //}

        //충돌한 오브젝트의 레이어가 "Bullet"인 경우                                   //!!! 레이어로 찾는 방법이 성능이 좋음 !!! ( 태그로 하는거보다 20배 빠름 ) 포폴에서 이거썼다고 하기
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Debug.Log("총알이 DestroyZone에 접근하여 파괴됨");

            //GameObject 배열을 썼을 경우 / 오브젝트 비활성화
            //other.gameObject.SetActive(false);
            
            // or 오브젝트 삭제
            //Destroy(other.gameObject);

            //List를 썼을 경우 poolSize가 넘어가면 계속 생성되므로 오브젝트 풀에 다시 넣어주는 과정이 필요
           /* pf.listBulletPool.Add(other.gameObject);*/                                                            // 참조하는 방법 1        

            //PlayerBulletFire pf = GameObject.Find("Player").GetComponent<PlayerBulletFire>();                 // 참조하는 방법 2 << 이거는 계속 닿을때마다 찾는 과정을 거치고 처리하는거라 비효율적

            //Queue를 썼을 경우
            pf.ReloadPool(other.gameObject);
        }
    }
}
