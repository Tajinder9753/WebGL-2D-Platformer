using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashingBlock : MonoBehaviour
{
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float fallSpeed;
    [SerializeField] LayerMask playerLayer;
    private bool isFalling = false;
    private Vector3 startPosition;
    Rigidbody2D rb;
    Animator anim;
    public float returnSpeed = 2f; 
    private bool isReturning = false;
    [SerializeField] GameObject smoke;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isFalling)
        {
            // Raycast downward to detect player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                anim.SetBool("isFalling", true);
                StartFalling();
            }
        }

    }

    //falls down towards the ground when player is beneath 
    private void StartFalling()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

    //returns back to position after hitting the ground 
    private void GoBack()
    {
        if (!isReturning)
            StartCoroutine(MoveBackToStart());
    }
    //co-routine to move back to starting position 
    private IEnumerator MoveBackToStart()
    {
        isReturning = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        while (Vector3.Distance(transform.position, startPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = startPosition; // Snap exactly to position
        isFalling = false;
        isReturning = false;
        anim.SetBool("hitGround", false);
    }
    //resets the bools when the block hits the ground 
    //begins moving back towards the starting position
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetBool("isFalling", false);
            anim.SetBool("hitGround", true);
            //creates and destroys the smoke effect when the block lands on the ground 
            GameObject effectInstance = (GameObject)Instantiate(smoke, transform.position-new Vector3(0,+2f,0), transform.rotation);
            Destroy(effectInstance, 2f);
            GoBack();
        }
    }

}
