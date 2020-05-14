using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Script used when the game is finished
 */
public class FinishedGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Loads the MainMenu scene when the player presses enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
