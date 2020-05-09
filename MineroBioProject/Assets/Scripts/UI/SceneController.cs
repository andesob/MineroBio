using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static string prevScene = "";
    public static string currentScene = "";

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene().name;

        if (prevScene == "levelHubScene" && currentScene == "HomeScene")
        {
            player.transform.position = new Vector2(0.5f, 22.3f);
        }
        if (prevScene == "Level1" && currentScene == "levelHubScene")
        {
            player.transform.position = new Vector2(-5f, -0.2f);
        }
    }


    public void LoadScene(string sceneName)
    {
        prevScene = currentScene;
        SceneManager.LoadScene(sceneName);
    }
}
