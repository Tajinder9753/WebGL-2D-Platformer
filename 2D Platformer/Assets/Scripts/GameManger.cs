using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] GameObject brownPlatform;
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    //resets the brown moving platform if the player gets hit or falls off and restarts at the checkpoint
   public void resetBrownPlatform()
    {
        brownPlatform.GetComponent<BrownMovingPlatform>().isMoving = false;
        brownPlatform.GetComponent<BrownMovingPlatform>().destinationReached = false;
        brownPlatform.GetComponent<BrownMovingPlatform>().numBalls = 1f;
        brownPlatform.GetComponent<BrownMovingPlatform>().countingTime = 0f;
        brownPlatform.transform.position = brownPlatform.GetComponent<BrownMovingPlatform>().initialSpot;
    }

}
