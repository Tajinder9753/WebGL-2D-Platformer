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

    private void Start()
    {
        Debug.Log("starting");
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isFalling)
        {
            // Raycast downward to detect player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                StartFalling();
            }
        }

    }

    private void StartFalling()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

}
