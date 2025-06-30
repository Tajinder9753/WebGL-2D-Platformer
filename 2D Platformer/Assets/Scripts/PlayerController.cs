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
    public Vector3 checkpoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
            anim.SetBool("isJumping", false);
        }
        if (collision.collider.tag == "trap")
        {
            // Trigger the "hit" animation
            transform.position = checkpoint;
            anim.SetBool("isHit", true);
            rb.bodyType = RigidbodyType2D.Static;
            // Start the coroutine to return to normal animation after a delay
            StartCoroutine(ResetToNormalAnimation());
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            checkpoint = new Vector3(collision.transform.position.x, collision.transform.position.y-1f, collision.transform.position.z);

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject startFlag = GameObject.FindGameObjectWithTag("start");
        checkpoint = new Vector3(startFlag.transform.position.x, startFlag.transform.position.y - 1.5f, startFlag.transform.position.z);
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

        if (transform.position.y <= -8)
        {
            transform.position = checkpoint;
            anim.SetBool("isHit", true);
            rb.bodyType = RigidbodyType2D.Static;
            // Start the coroutine to return to normal animation after a delay
            StartCoroutine(ResetToNormalAnimation());
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

    private IEnumerator ResetToNormalAnimation()
    {
        // Wait for the duration of the "hit" animation
        yield return new WaitForSeconds(3f);

        // Set the "isHit" bool to false to transition back to the normal animation
        anim.SetBool("isHit", false);
        rb.bodyType = RigidbodyType2D.Dynamic;

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
