using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;
    private bool isGrounded;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
            anim.SetBool("isJumping", false);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();

        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            anim.SetBool("isJumping", true);
            Jump();
            rb.gravityScale = 1;
        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            rb.gravityScale = 2;
        }
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (horizontalMovement > 0)
        {
            anim.SetBool("isRunning", true);
            GetComponent<SpriteRenderer>().flipX = false;
            Vector2 newVelocity;
            newVelocity.x = horizontalMovement;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }
        else if (horizontalMovement < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("isRunning", true);

            Vector2 newVelocity;
            newVelocity.x = horizontalMovement;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void Jump()
    {
        isGrounded = false;
        anim.SetBool("isGrounded", false);
        Vector2 newVelocity;
        newVelocity.x = rb.velocity.x;
        newVelocity.y = jumpForce;
        rb.velocity = newVelocity;

    }
}
