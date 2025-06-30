using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownMovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    private bool isMoving = false;
    [SerializeField] GameObject spikeBall;
    [SerializeField] Vector3 offset;
    private float delayTime = 3f;
    private float countingTime = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {

            // Set velocity based on direction
            if (isMoving)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                SpawnBall();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

            yield return null; // Wait until the next frame
        }
    }

    private void SpawnBall()
    {
        if (countingTime >= delayTime)
        {
            Instantiate(spikeBall, transform.position + offset, transform.rotation);
            countingTime = 0f;
        }

        countingTime += 1*Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isMoving = true;
        }

        if (collision.collider.tag == "barrier")
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }
    }
}
