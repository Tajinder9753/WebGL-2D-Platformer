using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayMovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private bool movingRight = true;

    //private void Start()
    //{
    //    StartCoroutine(MoveEnemy());
    //}

    //private IEnumerator MoveEnemy()
    //{
    //    while (true)
    //    {

    //        // Set velocity based on direction
    //        if (movingRight)
    //        {
    //            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    //        }
    //        else
    //        {
    //            Debug.Log("not moving right");
    //            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    //        }

    //        yield return null; // Wait until the next frame
    //    }
    //}

    private void Update()
    {
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
        {
            // Flip direction
            movingRight = !movingRight;
        }

        if (collision.collider.tag == "Player")
        {
            collision.transform.SetParent(transform); // Parent the player to the platform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null); // Parent the player to the platform

    }
}
