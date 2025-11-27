using UnityEngine;

/*
 유니티 어트리뷰트 [] 공부하기
https://docs.unity3d.com/ScriptReference/AddComponentMenu.html
 */

[RequireComponent(typeof(Rigidbody))]       //자동으로 원하는 컴포넌트를 추가/ 반드시 필요한 컴포넌트니까 실수방지용으로 깔아두는 것
/// <summary>
/// 1. 오른쪽에서 왼쪽으로 이동
/// 2. 플레이어를 향해 총알 발사
/// 3. 플레이어와 충돌하면 사라짐
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletFactory;
    
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //폭발효과
            //사운드
            //점수증가
            //적비행기사라짐
            //충돌한 오브젝트가 플레이어인 경우 처리
            Destroy(collision.gameObject);

            Destroy(this.gameObject,2f);        //2f는 2초후에 Destroy된다는 뜻
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "PetBullet")
        {
            Destroy(gameObject);
        }
    }
}
