using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalColliding3 : MonoBehaviour
{
    public player3 player3;
    public string colliderRole;


    void Start()
    {
        player3 = GetComponentInParent<player3>();
        colliderRole = "totalCollider";
    }

    private void OnTriggerEnter2D(Collider2D other) // 장애물 감지
    {
        if (player3 != null)
        {
            player3.HandleTriggerCollision(colliderRole, other);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // 벽 충돌 감지
    {
        Collider2D collider = collision.collider;

        if (player3 != null)
        {
            player3.HandleCollision(colliderRole, collider);
        }
    }
}
