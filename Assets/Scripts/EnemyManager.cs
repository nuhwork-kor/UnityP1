using UnityEditor.ShaderGraph.Internal;
using UnityEngine;



/// <summary>
/// 1. 적 비행기들을 관리하는 매니저 클래스
/// - 적 비행기 스폰
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyFactory;     //적 비행기 프리팹
    public Transform[] spawnPoints;     //적 비행기 스폰 위치들
    float spawnTime = 1f;               //적 스폰 주기(랜덤하게  변경해도 됨)
    //Vector3 enemyScale;              //적 크기
    float timer = 0;                    //스폰용 타이머
    void Update()
    {
        SpawnEnemy();                   //적 비행기 생성
    }

    void SpawnEnemy()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            ////스폰 위치 랜덤 선택
            //int index = Random.Range(0,spawnPoints.Length);
            //Vector3 spawnPos = spawnPoints[index].position;
            ////적 비행기 생성
            //Instantiate(enemyFactory, spawnPos, Quaternion.identity);
            ////타이머 초기화
            //timer = 0;
            ////다음 스폰 주기 랜덤 설정 (0.5 ~ 2초)
            //spawnTime = Random.Range(0.5f, 2f);

            timer = 0f;
            spawnTime = Random.Range(0.5f, 2f);
            GameObject enemy = Instantiate(enemyFactory);
            int index = Random.Range(0, spawnPoints.Length);
            enemy.transform.position = spawnPoints[index].position;
            Destroy(enemy, 5f);
        }
    }
}
