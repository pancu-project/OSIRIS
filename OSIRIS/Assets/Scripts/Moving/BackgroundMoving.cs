using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    Vector2 targetPos;
    [SerializeField] float length = 30.23f; // 이미지 길이

    private void Update()
    {
        targetPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

        if (transform.position.x + length < targetPos.x)
        {
            transform.position
                = new Vector2(transform.position.x + length * 2, transform.position.y);
        }

        // if (시체 조각 미회수 or 구역 재로드)
        //      배경 스크린 좌표를 카메라 좌표로 설정
    }
}