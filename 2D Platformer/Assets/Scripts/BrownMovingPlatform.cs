using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownMovingPlatform : MonoBehaviour
{
    //private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    public bool isMoving = false;
    [SerializeField] GameObject spikeBall;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 topOffset;
    private float delayTime = 3f;
    private float countingTime = 0;
    private float maxBalls = 4;
    private float numBalls = 1;
    public Vector3 initialSpot;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MovePlatform());
        initialSpot = transform.position;
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {

            // Set velocity based on direction
            if (isMoving)
            {
                //rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.position = new Vector2 (transform.position.x + speed*Time.deltaTime, transform.position.y);
                SpawnBall();
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y);
                Debug.Log("stop");
            }

            yield return null; // Wait until the next frame
        }
    }

    private void SpawnBall()
    {
        if (countingTime >= delayTime && numBalls <= maxBalls)
        {
            Instantiate(spikeBall, transform.position + offset, transform.rotation);
            Instantiate(spikeBall, transform.position + topOffset, transform.rotation);

            countingTime = 0f;
            numBalls++;
        }

        countingTime += 1*Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.SetParent(transform); // Parent the player to the platform
            isMoving = true;
        }

        if (collision.collider.tag == "barrier")
        {
            isMoving = false;
           // rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
