using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{

    public Animator anim;
    private int state = 1;
    AnimationEvent evt;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        evt = new AnimationEvent();
        if (this.gameObject.tag == "PlayButton")
        {
            anim.Play("Select");
        }

    }

    // Update is called once per frame

    void Update()
    {

        StateMachine();
    }

    public static class SceneChanger
    {
        public enum Scene
        {
            Game
        }

        public static void Load(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }

    private IEnumerator Wait()
    {
        anim.Play("Enter");
        yield return new WaitForSeconds(0.2f);
        SceneChanger.Load(SceneChanger.Scene.Game);
    }
    private IEnumerator Wait2()
    {
        anim.Play("Enter");
        yield return new WaitForSeconds(0.2f);
        Application.Quit();

    }

    private void StateMachine()
    {
        switch (state)
        {
            case 1:

                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (this.gameObject.tag == "PlayButton")
                    {
                        // anim.Play("Enter");

                        StartCoroutine("Wait");
                    }

                }
                if (Input.GetKeyDown(KeyCode.S))
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
                if (Input.GetKeyDown(KeyCode.W))
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
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (this.gameObject.tag == "SettingsButton")
                    {
                        // anim.Play("Enter");

                        StartCoroutine("Wait");

                        //  SceneChanger.Load(SceneChanger.Scene.Game);
                    }

                }
                if (Input.GetKeyDown(KeyCode.S))
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
                if (Input.GetKeyDown(KeyCode.W))
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
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (this.gameObject.tag == "QuitButton")
                    {
                        StartCoroutine("Wait2");
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
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
                if (Input.GetKeyDown(KeyCode.W))
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
                //code
                break;

        }
    }
}
