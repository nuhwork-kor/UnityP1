using UnityEngine;

public class PetBullet : MonoBehaviour
{
    public float speed = 10f;           //초당 10유닛 이동
    public float destroyDistance = 20f; //발사 위치로부터 20 유닛 떨어지면 삭제
    Vector3 startPosition;              //발사 위치

    public PetBulletFire owner;         //총알의 주인 정해주기(서로 나눠서 쓰지않기)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


}
