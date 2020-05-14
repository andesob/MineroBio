using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Script used to load new scene from the death menu
 */
public class DeathButtonClick : MonoBehaviour
{
    public void RetryButton(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1; 
    }

    public void QuitButton(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1; 
    }
}
