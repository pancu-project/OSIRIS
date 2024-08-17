using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float topPosition = 3f;
    [SerializeField] float bottomPosition = -3f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveDir = 1f;

    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigid.velocity = new Vector2(0, moveDir * moveSpeed);

        if (topPosition > bottomPosition)
        {
            if (transform.position.y >= topPosition)
            {
                moveDir = -1;
            }
            else if (transform.position.y <= bottomPosition)
            {
                moveDir = 1;
            }
        }
        else
        {
            if (transform.position.y >= bottomPosition)
            {
                moveDir = -1;
            }
            else if (transform.position.y <= topPosition)
            {
                moveDir = 1;
            }
        }
    }
}
