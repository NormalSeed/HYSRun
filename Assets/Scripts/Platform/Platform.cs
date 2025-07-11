using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : PooledObject
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    private void Start()
    {
        rb.gravityScale = 0f;
    }

    public void Launch(float y, float platformSpd)
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(20f, y);
        Vector2 direction = Vector2.left;
        rb.velocity = Vector2.left * platformSpd;
    }

    private void Update()
    {
        if (transform.position.x < -20)
        {
            ReturnPool();
        }
    }
}
