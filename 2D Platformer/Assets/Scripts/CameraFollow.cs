using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float heightOffset = 2;
    public float depth = -10;

    private void LateUpdate()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y + heightOffset,
            depth
            );
    }
}
