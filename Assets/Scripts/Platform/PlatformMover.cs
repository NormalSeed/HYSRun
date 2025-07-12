using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private float moveSpd;

    private void Awake()
    {
        moveSpd = 3f;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 destinationPos = new Vector2(-20f, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, destinationPos, moveSpd * Time.deltaTime);
    }
}
