using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    [SerializeField] float length = 30.23f; // �̹��� ����
    [SerializeField] float moveSpeed = 0f; // �̹��� ����
    Vector2 targetPos;
    Vector3 movePos;

    public PlayerAnimation playerAnimation;
    private Camera mainCamera;
    private void Start()
    {
        playerAnimation = GameObject.Find("Player").GetComponent<PlayerAnimation>();
        mainCamera = Camera.main; 
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

        //if (transform.position.x + length < targetPos.x)
        //{
        //    // transform.position
        //    //    = new Vector2(transform.position.x + length * 2, transform.position.y);
        //    transform.position = new Vector2(targetPos.x + length - length * 2, transform.position.y);
        //}
        //if (transform.position.x - length > targetPos.x )
        //{
        //    // transform.position
        //    //   = new Vector2(targetPos.x-length, transform.position.y);
        //    transform.position = new Vector2(targetPos.x - length + length * 2, transform.position.y);
        //}

        // ī�޶��� �� ���� Ȯ��
        float cameraLeftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        float cameraRightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        // ����� ī�޶��� �������� ����� �� ���ġ
        if (transform.position.x + length / 2 < cameraLeftEdge)
        {
            transform.position = new Vector2(transform.position.x + length * 2, transform.position.y);
        }
        // ����� ī�޶��� ���������� ����� �� ���ġ
        else if (transform.position.x - length / 2 > cameraRightEdge)
        {
            transform.position = new Vector2(transform.position.x - length * 2, transform.position.y);
        }
    }
        

        // if (��ü ���� ��ȸ�� or ���� ��ε�)
        //      ��� ��ũ�� ��ǥ�� ī�޶� ��ǥ�� ����
    
}