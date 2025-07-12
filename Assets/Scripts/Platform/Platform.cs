using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : PooledObject
{
    Rigidbody2D rb;
    private bool isLaunched;
    private Vector2 destinationPos;
    private float platformSpd;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();

    }

    private void Start()
    {
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {

            //transform.position = Vector2.MoveTowards(transform.position, destinationPos, platformSpd * Time.deltaTime);
            rb.MovePosition(Vector2.MoveTowards(rb.position, destinationPos, platformSpd * Time.fixedDeltaTime));

    }

    public void Launch(float y, float platformSpd)
    {
        this.platformSpd = platformSpd;
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(20f, y);
        rb.position = new Vector2(20f, y);
        destinationPos = new Vector2(-20f, y);

    }

    private void Update()
    {
        if (transform.position.x < -20)
        {
            ReturnPool();
        }
    }
}
