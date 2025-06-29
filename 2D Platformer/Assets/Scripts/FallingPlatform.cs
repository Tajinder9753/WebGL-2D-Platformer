using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.5f;
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private float fallDistance = 2f;
    [SerializeField] private float returnDelay = 2f;
    [SerializeField] private float returnSpeed = 2f;

    private Vector3 startPos;
    private bool isFalling = false;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.collider.CompareTag("Player"))
        {
            Invoke(nameof(StartFalling), fallDelay);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isFalling && collision.collider.CompareTag("Player"))
        {
            StartCoroutine(ReturnPlatform());
        }
    }

    private void StartFalling()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        isFalling = true;
        Vector3 targetPos = startPos + Vector3.down * fallDistance;
        moveCoroutine = StartCoroutine(MoveToPosition(targetPos, fallSpeed));
    }

    private IEnumerator ReturnPlatform()
    {
        yield return new WaitForSeconds(returnDelay);
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToPosition(startPos, returnSpeed));
        isFalling = false;
    }

    private IEnumerator MoveToPosition(Vector3 targetPos, float speed)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }
}
