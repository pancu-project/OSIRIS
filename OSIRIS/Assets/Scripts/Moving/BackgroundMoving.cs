using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    Vector2 targetPos;
    [SerializeField] float length = 30.23f; // �̹��� ����

    private void Update()
    {
        targetPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

        if (transform.position.x + length < targetPos.x)
        {
            transform.position
                = new Vector2(transform.position.x + length * 2, transform.position.y);
        }

        // if (��ü ���� ��ȸ�� or ���� ��ε�)
        //      ��� ��ũ�� ��ǥ�� ī�޶� ��ǥ�� ����
    }
}