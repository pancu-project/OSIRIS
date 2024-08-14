using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silding : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    public string colliderRole;

    void Start()
    {
        playerAnimation = GetComponentInParent<PlayerAnimation>();
        colliderRole = "sildeCollider";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerAnimation != null)
        {
            playerAnimation.HandleTriggerCollision(colliderRole, other);
        }
    }

}
