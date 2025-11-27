using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PetBulletFire : MonoBehaviour
{
    public GameObject petBulletFactory;
    public Transform firePoint;
    public GameObject clone;

    int poolSize = 5;

    Queue<GameObject> queueBulletPool;

    public float atktimer;
    public float atkSpd = 0.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitObjectPooling();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void InitObjectPooling()    //오브젝트 풀링 초기화
    {
        queueBulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            //총알 오브젝트 생성
            GameObject petBullet = Instantiate(petBulletFactory);
            //총알 오브젝트 비활성화
            petBullet.SetActive(false);
            //큐에 저장
            queueBulletPool.Enqueue(petBullet);
        }
    }

    void Fire()
    {
        //if (clone.activeSelf)
        //{
        //    atktimer += Time.deltaTime;
        //    if(atktimer > atkSpd)
        //    {
        //        atktimer = 0f;
        //        for(int i = 0; i < ;)
        //    }
        //}


        atktimer += Time.deltaTime;
        if (atktimer >= atkSpd)
        {
            if (queueBulletPool.Count > 0)
            {
                GameObject bullet = queueBulletPool.Dequeue();      //queue에서 꺼내서 대입해주고
                bullet.SetActive(true);
                bullet.transform.position = firePoint.position;
                bullet.transform.up = firePoint.right;

            }
            else
            {
                GameObject bullet = Instantiate(petBulletFactory);
                bullet.SetActive(false);
                queueBulletPool.Enqueue(bullet);

            }
            atktimer = 0;
        }
    }

    public void ReloadPool(GameObject obj)
    {
        obj.SetActive(false);
        queueBulletPool.Enqueue(obj);
    }
}
