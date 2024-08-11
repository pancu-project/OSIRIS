using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silding3 : MonoBehaviour
{
    public player3 player3;
    public string colliderRole;


    void Start()
    {
        player3 = GetComponentInParent<player3>();
        colliderRole = "sildeCollider";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player3 != null)
        {
            player3.HandleTriggerCollision(colliderRole, other);
        }
    }
}
