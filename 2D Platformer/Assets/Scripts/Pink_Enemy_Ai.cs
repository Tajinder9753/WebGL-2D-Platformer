using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pink_Enemy_Ai : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
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
                    GetComponent<SpriteRenderer>().flipX = false;
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }

            yield return null; // Wait until the next frame
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
        {
            // Flip direction
            movingRight = !movingRight;
        }
    }
}
