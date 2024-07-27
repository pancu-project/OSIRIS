using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObstacleMovement : MonoBehaviour
{
    [SerializeField] float playerDistance = 10f;
    [SerializeField] float moveSpeed = 20f;

    private float height;
    private BoxCollider2D collider;
    private Vector2 targetPos;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        height = collider.size.y;

        targetPos = transform.position;
        transform.position = new Vector2(transform.position.x, transform.position.y + height);
    }

    private void Update()
    {
        if (transform.position.x - player.transform.position.x < playerDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}
