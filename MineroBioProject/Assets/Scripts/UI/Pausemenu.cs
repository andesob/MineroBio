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
    public Button quitButton;
    public Button settingsButton;
        
    private int state;
    private bool inMenu = false;
    public bool isPaused = false;
    public bool canPause = true;

    private float currentTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        state = 1;
        currentTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && canPause)
        {
            Pause();
            isPaused = true;
        }
        if (inMenu)
        {
            menuOptions();
        }
    }

    public void Pause()
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
        isPaused = false;
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
                settingsButton.Select();
                if (Input.GetKeyDown(KeyCode.W))
                {
                    changeState(1);
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    changeState(3);
                }
                else if (Input.GetKeyDown(KeyCode.Return) && Time.time > currentTime)
                {
                    print("hello");
                    pauseMenu.SetActive(false);
                    inMenu = false;
                    this.gameObject.GetComponent<SettingsMenu>().startSettings();
                }
                break;

            case 3:
                quitButton.Select();
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    changeState(2);
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

    public void wait(float time) 
    {
        currentTime = time + 0.1f;
    }
}
