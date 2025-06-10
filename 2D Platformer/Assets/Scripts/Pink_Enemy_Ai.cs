using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pink_Enemy_Ai : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    [SerializeField] float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2 (-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 1 && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 1 && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
}
