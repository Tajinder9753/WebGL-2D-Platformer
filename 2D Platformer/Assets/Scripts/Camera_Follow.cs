using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float height = 2;
    public float depth = -10;
    
    //follows the player as they move around horizontally (x-axis) 
    private void LateUpdate()
    {
        transform.position = new Vector3(
            target.position.x,
            height,
            depth
            );
    }
}
