using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;
    private bool isGrounded;
    public Vector2 checkpoint;
    [SerializeField] GameObject gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //allows jumping if on the ground through isGrounded bool
        if (collision.collider.tag.Equals("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
            anim.SetBool("isJumping", false);
        }
        //resets player to latest checkpoint when hit by a trap or spikeBall 
        if (collision.collider.tag == "trap" || collision.collider.tag == "spike")
        {
            transform.SetParent(null);
            transform.position = checkpoint;
            anim.SetBool("isHit", true);
            //prevents the player from moving while hit animation is playing 
            rb.bodyType = RigidbodyType2D.Static;

            //destroys any spikeBalls that may be around if the player gets hit while on the platform
            GameObject[] spikeBalls = GameObject.FindGameObjectsWithTag("spike");
            foreach (GameObject spikeBall in spikeBalls) {
                Destroy(spikeBall);
            }
            gameManager.GetComponent<GameManger>().resetBrownPlatform();

            // Start the coroutine to return to normal animation after a delay
            StartCoroutine(ResetToNormalAnimation());
        }
        //loads the end game screen if player touches the trophy
        if(collision.collider.tag == "trophy")
        {
            SceneManager.LoadScene(2);
        }

    }
    //saves the checkpoint location when player activates it 
    private void OnTriggerEnter2D(Collider2D collision)
    {
            checkpoint = new Vector2(transform.position.x, transform.position.y);

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpoint = new Vector2(transform.position.x, transform.position.y);
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
        //falls faster when jump button not pressed 
        //allows for long jump when button held 
        if (Input.GetAxisRaw("Jump") == 0)
        {
            rb.gravityScale = 2;
        }

        //if player falls off the map, will reset their position to the latest checkpoint 
        if (transform.position.y <= -8)
        {
            transform.position = checkpoint;
            anim.SetBool("isHit", true);
            //prevents the player from moving while hit animation is playing 
            rb.bodyType = RigidbodyType2D.Static;

            //destroys any spikeBalls that may be around if the player gets hit while on the platform
            GameObject[] spikeBalls = GameObject.FindGameObjectsWithTag("spike");
            foreach (GameObject spikeBall in spikeBalls)
            {
                Destroy(spikeBall);
            }
            gameManager.GetComponent<GameManger>().resetBrownPlatform();
            // Start the coroutine to return to normal animation after a delay
            StartCoroutine(ResetToNormalAnimation());
        }
    }

    //moves player character 
    //flips the sprite when moving to the left 
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

    //co-routine to play the hit animation after the player gets hit 
    //returns control to the player after designated amount of time 
    private IEnumerator ResetToNormalAnimation()
    {
        // Wait for the duration of the "hit" animation
        yield return new WaitForSeconds(3f);

        // Set the "isHit" bool to false to transition back to the normal animation
        anim.SetBool("isHit", false);
        rb.bodyType = RigidbodyType2D.Dynamic;

    }

    //jumps when jump button pressed 
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
