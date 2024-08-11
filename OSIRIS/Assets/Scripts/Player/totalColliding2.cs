using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalColliding2 : MonoBehaviour
{
    public player2 player2;
    public string colliderRole;


    void Start()
    {
        player2 = GetComponentInParent<player2>();
        colliderRole = "totalCollider";
    }

    private void OnTriggerEnter2D(Collider2D other) // ��ֹ� ����
    {
        if (player2 != null)
        {
            player2.HandleTriggerCollision(colliderRole, other);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // �� �浹 ����
    {
        Collider2D collider = collision.collider;

        if (player2 != null)
        {
            player2.HandleCollision(colliderRole, collider);
        }
    }
}
