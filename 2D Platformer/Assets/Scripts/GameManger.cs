using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] GameObject brownPlatform;
    public void starGame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Application.Quit();
    }

   public void resetBrownPlatform()
    {
        brownPlatform.GetComponent<BrownMovingPlatform>().isMoving = false;
        brownPlatform.transform.position = brownPlatform.GetComponent<BrownMovingPlatform>().initialSpot;
    }

}
