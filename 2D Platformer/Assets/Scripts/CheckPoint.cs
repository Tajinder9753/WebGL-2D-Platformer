using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        // Cache the Animator component on start
        anim = GetComponent<Animator>();
    }
    //when the player passes the checkpoint will trigger the animation
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Play the checkpoint animation (triggered once)
        anim.SetBool("Check", true);

    }
}
