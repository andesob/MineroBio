using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    public Button retryButton;
    public Button quitButton;

    private enum State
    {
        Retry,
        Quit,
    }
    private State state;
    
    private void Start()
    {
        state = State.Retry;
    }

    private void Update()
    {
        MenuOptions();
    }


    public void MenuOptions()
    {
        switch (state)
        {
            case State.Retry:
                 retryButton.Select();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Retry();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    ChangeState(State.Quit);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    ChangeState(State.Quit);
                }
                break;

            case State.Quit:
                quitButton.Select();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    QuitToMainMenu();
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    ChangeState(State.Retry);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    ChangeState(State.Retry);
                }
                break;

            default:
                break;
        }
    }

    private void Retry()
    {
        SceneManager.LoadScene("Scenes/HomeScene");
        Time.timeScale = 1;
    }

    private void QuitToMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
        Time.timeScale = 1;
    }

    private void ChangeState(State newState)
    {
         EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
         state = newState;
    }

}
