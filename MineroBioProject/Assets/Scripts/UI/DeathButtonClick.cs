using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathButtonClick : MonoBehaviour
{
    public void RetryButton(string scene)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(scene);
        Time.timeScale = 1; 
    }

    public void QuitButton(string scene)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(scene);
        Time.timeScale = 1; 
    }
}
