using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownMovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] GameObject spikeBall;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 topOffset;
    private float delayTime = 3f;
    private float countingTime = 0;
    private float maxBalls = 4;
    private float numBalls = 1;
    public bool isMoving = false;
    public bool destinationReached = false;
    public Vector3 initialSpot;

    //starts the co-routine and saves the initial position 
    private void Start()
    {
        StartCoroutine(MovePlatform());
        initialSpot = transform.position;
    }

    //co-routine to move the platform once the player steps on the platform
    private IEnumerator MovePlatform()
    {
        while (true)
        {

            if (isMoving)
            {
                transform.position = new Vector2 (transform.position.x + speed*Time.deltaTime, transform.position.y);
                SpawnBall();
            }
            //comes to a stop when colliding with the stopPoint 
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y);
            }

            yield return null; 
        }
    }

    //spawns a spikeBall trap that moves towards the player while on the platform
    //stops when the number of maxBalls is hit
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
    //sets isMoving to true when player steps on the platform to start the co-routine 
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //destinationReached used to ensure platform does not start moving again if player jumps back on when it reaches the stopPoint
        if (collision.collider.tag == "Player" && destinationReached == false)
        {
            // Parent the player to the platform, to enhance movement while on the platform
            collision.transform.SetParent(transform);
            isMoving = true;
        }

        if (collision.collider.tag == "barrier")
        {
            destinationReached = true;
            isMoving = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //unparents the player to the platform when they get off 
        collision.transform.SetParent(null);
    }
}
