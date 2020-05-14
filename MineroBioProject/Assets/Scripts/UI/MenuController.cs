using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

/*
 * Script used in the main menu to control what happens when the buttons are pressed
 */
public class MenuController : MonoBehaviour
{
    public Animator anim;

    private int state = 1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (gameObject.CompareTag("PlayButton"))
        {
            anim.Play("Select");
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    //Play animation before starting
    private IEnumerator PlayWait()
    {
        anim.Play("Enter");
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<SceneChanger>().ChangeScene("HomeScene");
    }

    //Play animation before quitting
    private IEnumerator QuitWait()
    {
        anim.Play("Enter");
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }

    private void StateMachine()
    {
        //Statemachine to alternate between the buttons
        switch (state)
        {
            case 1:

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (this.gameObject.tag == "PlayButton")
                    {
                        StartCoroutine("PlayWait");
                    }
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (this.gameObject.tag == "SettingsButton")
                    {
                        anim.Play("Select");
                    }

                    if (this.gameObject.tag == "PlayButton")
                    {
                        anim.Play("Deselect");
                    }
                    state = 2;
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (this.gameObject.tag == "QuitButton")
                    {
                        anim.Play("Select");
                    }

                    if (this.gameObject.tag == "PlayButton")
                    {
                        anim.Play("Deselect");
                    }
                    state = 3;
                }
                break;

            case 2:

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (this.gameObject.tag == "SettingsButton")
                    {
                        StartCoroutine("PlayWait");
                    }
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (this.gameObject.tag == "QuitButton")
                    {
                        anim.Play("Select");
                    }

                    if (this.gameObject.tag == "SettingsButton")
                    {
                        anim.Play("Deselect");
                    }
                    state = 3;
                }

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (this.gameObject.tag == "SettingsButton")
                    {
                        anim.Play("Deselect");
                    }

                    if (this.gameObject.tag == "PlayButton")
                    {
                        anim.Play("Select");
                    }
                    state = 1;
                }
                break;

            case 3:

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (this.gameObject.tag == "QuitButton")
                    {
                        StartCoroutine("QuitWait");
                    }
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (this.gameObject.tag == "PlayButton")
                    {
                        anim.Play("Select");
                    }

                    if (this.gameObject.tag == "QuitButton")
                    {
                        anim.Play("Deselect");
                    }
                    state = 1;
                }

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (this.gameObject.tag == "SettingsButton")
                    {
                        anim.Play("Select");
                    }

                    if (this.gameObject.tag == "QuitButton")
                    {
                        anim.Play("Deselect");
                    }
                    state = 2;
                }
                break;

            default:
                break;
        }
    }
}
