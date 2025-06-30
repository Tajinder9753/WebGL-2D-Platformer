using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayMovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 8f;
    private bool movingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy()
    {
        while (true)
        {

            // Set velocity based on direction
            if (movingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            yield return null; // Wait until the next frame
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "barrier")
        {
            // Flip direction
            movingRight = !movingRight;
        }
    }
}
