using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 movePos = new Vector3(moveSpeed * Time.deltaTime, 0);

        transform.Translate(movePos);
        mainCamera.transform.Translate(movePos);
    }
}
