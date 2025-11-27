using Unity.VisualScripting;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    /*필요할거같은거
    일단 펫은 trigger
    1)처음 플레이어의 위치 (저장용) /나중 플레이어의 위치 (펫 이동을 위한 변수?)
    2)그냥 현재 플레이어 위치만 읽기
    if(플레이어와의 Distance > 최대간격)
    {
        Lookat(Transform 플레이어위치)
        transform.Translate
    }

    아니면
    플레이어와의 최대간격 0(플레이어) < 펫(요사이에 있어야함) < 최대간격
    이동속도
    */

    public float petMoveSpeed = 5f;
    public float maxInterval = 3f;
    public GameObject Target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        petMove();
    }
    
    void petMove()
    {
        float currentDis = (Target.transform.position - transform.position).sqrMagnitude;
         

        if (currentDis > maxInterval * maxInterval)
        {
            //transform.LookAt(Player.transform.position);      //이걸 안 쓴 이유는, 탄막게임에서 펫이 공격을 할 경우 정면을 항상 바라봐야하기 때문에 정면으로 공격이나가는데  플레이어를 바라보게되면 어색해짐

            //MoveTowards사용, 근데 너무 뻣뻣하게 움직임. 부드럽게 딸려오는 느낌이 좋을거같은데
            transform.position = Vector3.MoveTowards(
                transform.position,
                Target.transform.position,
                petMoveSpeed * Time.deltaTime
                );
        }
            
    }
}
//setactiveself 활성화 상태 확인
//오브젝트 풀링 사용하기
// if(활성화가 되어있으면)
//{
//  petShoot();
//}
//void ToggleInventory()
//{
//  bool isActive = inventoryPanel.activeSelf;
//  inventoryPanel.SetActive(!isActive); // 한번만 적용
//
//  if (!isActive) UpdateInventoryUI();
//  }
//}