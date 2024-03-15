using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.isDead = false;
        GameManager.Instance.levelComplete = false;
        GameManager.Instance.timeIsRunningOut = false;
        GameManager.Instance.levelComplete = false;
        GameManager.Instance.gameOver = false;

    }
}
