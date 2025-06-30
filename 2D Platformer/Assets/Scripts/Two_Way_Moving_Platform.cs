using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayMovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private bool movingRight = true;

    //bounces back and forth between two barriers with the movingRight bool deciding the direction 
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
        //flips direction when colliding with any object besides the Player
        if (collision.collider.tag != "Player")
        {
            // Flip direction
            movingRight = !movingRight;
        }

        if (collision.collider.tag == "Player")
        {
            //Parents the player to the platform to enhance the movement while player is on the platform
            collision.transform.SetParent(transform); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // unparents the player to the platform once they get off
        collision.transform.SetParent(null);

    }
}
