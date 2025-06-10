using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int fallDelay;
    private float gravityScale =  0.1f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(Falling), fallDelay);
        
    }

    private void Falling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale;
        Destroy(this, 5f);
    }
}
