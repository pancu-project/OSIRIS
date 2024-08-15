using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 7f;

    Camera mainCamera;
    Vector3 movePos;

    //Vector3 cameraPosition;
    private float offsetX = -3f;
    public bool heart = false; 

    private PlayerAnimation playerAnimation;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        movePos = new Vector3(moveSpeed * Time.deltaTime, 0);
        transform.Translate(movePos);

        // 플레이어가 카메라 뷰를 벗어났는지 확인
        CheckIfPlayerOutOfCameraView();
    }

    private void CheckIfPlayerOutOfCameraView()
    {
        Vector3 playerViewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (playerViewportPosition.x < 0.2 || playerViewportPosition.x > 1)
        {
            //Debug.Log("Player is out of camera view!");
            //if (!heart)
            //{
            //    playerAnimation.minusHeart();
            //    heart = true;
            //}

            Vector3 catchUpCameraPosition = new Vector3(transform.position.x + offsetX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, catchUpCameraPosition, moveSpeed * Time.deltaTime);

        }
        //else
        //{
        //    heart = false;
        //}

    }

    private void LateUpdate()
    {
        mainCamera.transform.Translate(movePos);
    }
}
