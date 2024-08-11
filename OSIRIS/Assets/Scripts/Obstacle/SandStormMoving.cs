using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStormMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = -3f;
    [SerializeField] float distance = 5f;

    private float gap; // �÷��̾�� ��ֹ� ���� ����
    private GameObject player;
    private Vector3 location; // ��ֹ��� ���� ��ġ

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        location = transform.position;
    }

    private void Update()
    {
        gap = transform.position.x - player.transform.position.x;

        if (Mathf.Abs(gap) < distance)
        {
            transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
        }
        else
        {
            transform.position = location;
        }
    }
}
