using UnityEngine;


/// <summary>
/// 1. 배경 스크롤
/// </summary>
public class Background : MonoBehaviour
{
    Material mat;                       //배경 이미지 매터리얼
    public float scrollSpeed = 0.1f;    //스크롤 속도


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //배경이미지 매터리얼 가져오기
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //백그라운드 스크롤
        ScrollBackground();
    }

    void ScrollBackground()
    {
        // 시간에 따른오프셋 계산
        Vector2 offset = mat.mainTextureOffset;
        offset.Set(offset.x * scrollSpeed * Time.deltaTime, 0);
        mat.mainTextureOffset = offset;
    }
}
