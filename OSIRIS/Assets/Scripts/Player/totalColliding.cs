using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalColliding : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
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
}
