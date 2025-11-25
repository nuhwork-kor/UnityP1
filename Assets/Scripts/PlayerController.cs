using System.Timers;
using UnityEngine;


/// <summary>
/// 1. 플레이어 이동
/// 2. 플레이어 화면 밖으로 못나가게 막기
/// </summary>
public class PlayerController : MonoBehaviour
{
    //플레이어 이동 구현하기
    //Transform.position
    //Transform.Transflate()
    Rigidbody rb;

    public float baseSpeed = 5f;

    public Vector3 dir;                 //rigidbody에 사용

    Vector3 targetPosition;             //목표위치 Move3 에서 사용

    private Camera mainCamera;

    [SerializeField] float paddingX = 0.03f;       //UV 좌표는 0 ~ 1 사이, 작은 값으로 수치 조정해야 함.
    [SerializeField] float paddingY = 0.05f;          //public으로 해놓으면 인스펙터에서 값을 기억하고 있어서 Inspector에서 직접 또 바꿔줘야함
    // UI 요소 중 마진과 패딩이 있음
    // 마진은 가장 바깥, 그 안쪽 패딩, 중앙 콘텐츠


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();       //RigidBody이용

        //초기 목표 위치를 현재 위치로 설정 <- Move3에 활용
        targetPosition = transform.position;

        //메인 카메라 참조 가져오기
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        //GetKeyInput();        //RigidBody이용
        //Move1();              //transform.position 이용
        //Move2();              //Translate 이용
        //Move3();                //MoveTowards 이용
        Move4();

        MoveInScreen();         //캐릭터 스크린 안에 가두기
    }

    private void FixedUpdate()
    {
        //MovePlayer();       //RigidBody이용, 물리이동이라 FixedUpdate에 적용.
    }


    void Move1()        //GetAxis는 캐릭 밀림 있음
    {
        // 1. 입력 받기 (키보드, 마우스 등 입력은 Input 매니저가 담당한다)
        // Input.GetAxis("Horizontal") -> -1 ~ 1        (실수) -1 ~ 소수 ~ 0 ~ 소수 ~ 1 라서 !!!캐릭터 밀림현상이 있음. 근데 자연스러움!!!
        // Input.GetAxisRaw("Horizontal") -> -1 ~ 1     (정수) -1, 0, 1 이라서 딱딱 끊겨서 !!!캐릭터 밀림현상이 없음!!!

        float moveX = Input.GetAxisRaw("Horizontal");  //좌우 입력
        float moveY = Input.GetAxisRaw("Vertical");    //상하 입력

        // 2. 이동 벡터 만들기
        Vector3 moveDir = new Vector3(moveX, moveY, 0);    //z축 0으로 고정

        // 3. 방향 벡터 정규화 (대각선 이동시 속도 보정)
        moveDir = moveDir.normalized;

        // 4. 이동
        //Vector3 pos = transform.position + moveDir * baseSpeed * Time.deltaTime;
        //transform.position = pos;  아래는 줄여서 쓰는 방법
        transform.position += moveDir * baseSpeed * Time.deltaTime;
    }

    void Move2()        //캐릭터 밀림 없음
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(moveX, moveY, 0);
        moveDir.Normalize();
        transform.Translate(moveDir * baseSpeed * Time.deltaTime);
        //Translate는 로컬좌표계 기준 이동
        //Space.World는 월드좌표계 기준 이동 ( 게임 세계의 절대 좌표 )
        //Space.Self는 로컬좌표계 기준 이동 ( 객체 자신의 좌표 - 자신의 방향 기준 ) //부모 자식간의 관계에서 자식꺼를 따로 처리하고 싶을 때 Space.Self를 보통 씀
        //transform.Translate(moveDir * baseSpeed * Time.deltaTime, Space.World);  
    }

    void Move3()        //캐릭터 밀림 없음, 아주 약간 부드러움 , 카메라 고정 시키면 끝에가서 움직이면 걸림현상 생김(간만큼
    {
        //MoveTowards 는 현재 위치에서 목표 위치로 일정 속도로 이동
        //MoveTowards(현재위치, 목표위치, 속도 * 시간)

        // 입력 받기
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 입력이 있으면 목표 위치 갱싱
        if (moveX != 0f || moveY != 0f)
        {
            Vector3 moveDir = new Vector3(moveX, moveY, 0);
            moveDir.Normalize();
            targetPosition += moveDir * baseSpeed * Time.deltaTime; //1유닛 이동한 위치를 

            //현재 위치에서 목표 위치로 이동
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                baseSpeed * Time.deltaTime);
        }
    }


    void Move4()
    {
        // 메인 카메라의 주요 함수
        // 자주 사용하기 때문에 어디서든 접근할 수 있도록 변수 선언해서 사용
        // Camera.main: 씬에서 "MainCamera"태그가 붙은 카메라를 찾아서 반환

        // 1. ScreenToWorldPoint : 화면 좌표를 월드 좌표로 변환
        // 2. ScreenToViewportPoint : 화면 좌표를 뷰포트 좌표로 변환
        // 3. ViewportToWorldPoint : 뷰포트 좌표를 월드 좌표로
        // 4. WorldToScreenPoint : 월드 좌표를 화면 좌표로
        // 5. WorldToViewportPoint : 월드 좌표를 뷰포트 좌표로 
        // 6. ViewportToScreenPoint : 뷰포트 좌표를 스크린 좌표로
        // 뷰포트 좌표 : (0, 0) 왼쪽아래, (1, 1)오른쪽 위

        //스크린의 화면을 마우스로 클릭 했을 때 3D 공간의 클릭 지점으로 오브젝트를 움직일 때
        // Camera.main.ScreenToWorldPoint(Input.mousePosition)

        //마우스 왼쪽 버튼을 누르고 있는 동안 계속 움직이게 => ButtonDown x / Button o
        if (Input.GetMouseButton(0))
        {
            // 마우스 스크린 좌표를 월드 좌표로 변환
            // 플레이어 높이(z)는 유지 해줘야 함.
            Vector3 mousePos = Input.mousePosition;    //스크린 좌표
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = transform.position.z;      //z축 고정 

            print("마우스 클릭 좌표 : " + mousePos);
            print("월드 좌표 : " +  worldPos);
            print("플레이어 월드 좌표 : " + transform.position);

            //만약 한번의 클릭으로 클릭 좌표까지 이동하려면 MoveTowards()함수가 업데이트에 있어야 함
            //근데 업데이트로 뺄 때는 위에 Vector3 mousePos, worldPos를 class 필드에 따로 빼주고 해야 함.
            transform.position = Vector3.MoveTowards(
                transform.position,
                worldPos,
                baseSpeed * Time.deltaTime);
        }
    }

    void MoveInScreen()
    {
        //플레이어를 화면 밖으로 나가지 못하게 막기
        // 1. 화면 밖 공간에 큐브 4개 만들어서 배치하면 충돌체 때문에 밖으로 벗어나지 못함
        // 2. 플레이어 transform.position의 X, Y 값을 고정시킨다.
        //Vector3 position = transform.position;
        //if (position.x >= 5) position.x = 5;
        //if (position.y >= 3) position.y = 3;

        ////위의 코드보다는 유니티에서는 Clamp 사용 권장함.
        //position.x = Mathf.Clamp(position.x, -8.5f, 8.5f);
        //position.y = Mathf.Clamp(position.y, -3.5f, 3.5f);

        // 3. 메인카메라의 뷰포트를 가져와서 처리한다
        // 스크린 좌표 : 모니터 해상도의 픽셀 (1920 * 1080 < 만큼의 픽셀)
        // 뷰포트 좌표 : 카메라의 사각뿔 끝에 있는 사각형 / 왼쪽 아래 (0, 0) 오른쪽 위 (1, 1)
        // UV 좌표(화면 텍스트, 2D 이미지를 표시하기 위한 좌표계/ 텍스쳐 좌표계라고도 함) 왼쪽 위 (0, 0) 오른쪽 아래(1, 1)
         
        Vector3 position = mainCamera.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp(position.x, 0f + paddingX, 1f - paddingX);           //
        position.y = Mathf.Clamp(position.y, 0f + paddingY, 1f - paddingY);
        transform.position = mainCamera.ViewportToWorldPoint(position);

    }


    void GetKeyInput()        //RigidBody이용
    {
        dir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow)) dir.y += 1;
        if (Input.GetKey(KeyCode.DownArrow)) dir.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow)) dir.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow)) dir.x += 1;

        dir = dir.normalized;
    }

    void MovePlayer()         //RigidBody이용
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = baseSpeed * dir * Time.deltaTime;
        rb.MovePosition(currentPos + targetPos);
    }
}


