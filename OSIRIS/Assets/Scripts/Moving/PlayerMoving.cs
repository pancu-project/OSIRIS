using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;

    Camera mainCamera;
    Vector3 movePos;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        movePos = new Vector3(moveSpeed * Time.deltaTime, 0);
        transform.Translate(movePos);
    }

    private void LateUpdate()
    {
        mainCamera.transform.Translate(movePos);
    }
}
