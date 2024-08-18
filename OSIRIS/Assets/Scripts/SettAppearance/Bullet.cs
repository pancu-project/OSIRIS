using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    Rigidbody2D rigid;
    Vector2 launchDir;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        rigid = GetComponent<Rigidbody2D>();

        launchDir = ((Vector2)target.position - 
            (Vector2)transform.position).normalized;
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        rigid.velocity = launchDir * 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject, .5f);
    }
}