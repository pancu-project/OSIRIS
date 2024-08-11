using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalColliding : MonoBehaviour
{
    public PlayerAnimation playerAnimation;
    public string colliderRole;


    void Start()
    {
        playerAnimation = GetComponentInParent<PlayerAnimation>();
        colliderRole = "totalCollider";
    }

    private void OnTriggerEnter2D(Collider2D other) // 장애물 감지
    {
        if (playerAnimation != null)
        {
            playerAnimation.HandleTriggerCollision(colliderRole, other);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // 벽 충돌 감지
    {
        Collider2D collider = collision.collider;

        if (playerAnimation != null)
        {
            playerAnimation.HandleCollision(colliderRole, collider);
        }
    }
}
