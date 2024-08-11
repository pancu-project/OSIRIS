using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStormMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = -3f;
    [SerializeField] float distance = 5f;

    private float gap; // 플레이어와 장애물 사이 간격
    private GameObject player;
    private Vector3 location; // 장애물의 원래 위치

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
