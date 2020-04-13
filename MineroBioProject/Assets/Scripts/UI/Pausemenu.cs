using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausemenu : MonoBehaviour
{
    public Animator anim;
    public GameObject pauseMenu;

    public Button resumeButton;
    public Button inventoryButton;
    public Button quitButton;
    public Button settingsButton;

    private int state;
    private bool inMenu = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        state = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (inMenu)
        {
            menuOptions();
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PlayerController.isGamePaused = true;
        inMenu = true;
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PlayerController.isGamePaused = false;
        inMenu = false;
    }

    public void menuOptions()
    {
        switch (state)
        {
            case 1:
                resumeButton.Select();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Resume();
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    changeState(2);
                }
                break;

            case 2:
                inventoryButton.Select();
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    changeState(1);
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    changeState(3);
                }
                break;

            case 3:
                settingsButton.Select();
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    changeState(2);
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    changeState(4);
                }
                break;

            case 4:
                quitButton.Select();
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    changeState(3);
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    Time.timeScale = 1f;
                    quitToMenu();
                }
                break;
            default:
                break;
        }
    }

    private void quitToMenu() { SceneManager.LoadScene("Scenes/MainMenu"); }

    private void changeState(int newState)
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        state = newState;
    }
}
