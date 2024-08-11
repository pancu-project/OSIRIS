using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silding2 : MonoBehaviour
{
    public player2 player2;
    public string colliderRole;


    void Start()
    {
        player2 = GetComponentInParent<player2>();
        colliderRole = "sildeCollider";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player2 != null)
        {
            player2.HandleTriggerCollision(colliderRole, other);
        }
    }
}
