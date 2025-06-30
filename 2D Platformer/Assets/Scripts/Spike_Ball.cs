using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;

    //starts the co-routine, destroying the object after 10 seconds
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveBall());
        Destroy(gameObject, 10f);
    }
    //co-routine that moves the spikeBall to the left, towards the player 
    private IEnumerator MoveBall ()
    {
        while (true)
        {

           rb.velocity = new Vector2(-speed, rb.velocity.y);

           yield return null; // Wait until the next frame
        }
    }
}
