using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerModel model;
    PlayerView view;
    Vector2 moveVelocity;
    Rigidbody2D rb;

    private void Awake() => Init();

    private void Init()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();
        rb = GetComponent<Rigidbody2D>();

        model.CurHP.Value = model.MaxHP;
    }

    private void Update()
    {
        MoveInput();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);
    }

    void MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        moveVelocity = new Vector2(x, 0).normalized * model.MoveSpd;
    }

    void Jump()
    {
        Debug.Log("มกวมวิ");
        rb.AddForce(Vector2.up * model.JumpPower * rb.gravityScale, ForceMode2D.Impulse); 
    }
}
