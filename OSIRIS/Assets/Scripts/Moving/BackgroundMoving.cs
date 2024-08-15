using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    [SerializeField] float length = 30.23f; // 이미지 길이
    [SerializeField] float moveSpeed = 0f; // 이미지 길이
    Vector2 targetPos;
    Vector3 movePos;

    public PlayerAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = GameObject.Find("Player").GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        targetPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        movePos = new Vector3(moveSpeed * Time.deltaTime, 0);

        if (playerAnimation.isFly)
        {
            movePos = new Vector3(-15f * Time.deltaTime, 0);
            transform.Translate(movePos);
        }
        else
        {
            movePos = new Vector3(moveSpeed * Time.deltaTime, 0);
            transform.Translate(movePos);
        }

        if (transform.position.x + length < targetPos.x)
        {
            transform.position
                = new Vector2(transform.position.x + length * 2, transform.position.y);
        }

        // if (시체 조각 미회수 or 구역 재로드)
        //      배경 스크린 좌표를 카메라 좌표로 설정
    }
}