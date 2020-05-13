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
            player.transform.position = new Vector2(-4.9f, -4.2f);
        }
        if (prevScene == "Level2" && currentScene == "levelHubScene")
        {
            player.transform.position = new Vector2(-4.9f, -8.2f);
        }
        if (prevScene == "Level3" && currentScene == "levelHubScene")
        {
            player.transform.position = new Vector2(-4.9f, -12.2f);
        }
        if (prevScene == "Tutorial" && currentScene == "HomeScene")
        {
            player.transform.position = new Vector2(11f, 3.2f);
        }
    }


    public void LoadScene(string sceneName)
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {

        GameObject.Find("GameManager").GetComponent<GameManager>().SetHealth(HeartsHealthVisual2.heartsHealthSystemStatic.getTotalFragments());
        }
        prevScene = currentScene;
        SceneManager.LoadScene(sceneName);
    }
}
