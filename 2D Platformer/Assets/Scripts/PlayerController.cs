using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce;
    private bool isGrounded;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            isGrounded = true;
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            rb.gravityScale = 1;
        }
        
        if (Input.GetButtonUp("Jump")) //| rb.velocity.y < -0.1)
        {
            rb.gravityScale = 2;
        }
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
        Vector2 newVelocity;
        newVelocity.x = horizontalMovement;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
    }

    private void Jump()
    {
        isGrounded = false;
        Vector2 newVelocity;
        newVelocity.x = rb.velocity.x;
        newVelocity.y = jumpForce;
        rb.velocity = newVelocity;

    }
}
