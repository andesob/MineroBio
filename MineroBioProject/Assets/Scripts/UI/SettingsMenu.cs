using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Script used to control the settings menu ingame. Used to change the volume
 */
public class SettingsMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject settingsMenu;
    public Button backButton;
    public Button volumeButton;
    public Slider slider;
    public AudioMixer audioMixer;

    private int state;
    private bool inSettings;

    private void Start()
    {
        anim = GetComponent<Animator>();
        inSettings = false;
        state = 1;
    }

    void Update()
    {
        if (inSettings)
        {
            settingsOptions();
        }
    }

    public void settingsOptions()
    {
        switch (state)
        {
            case 1:
                backButton.Select();

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    this.gameObject.GetComponent<Pausemenu>().wait(Time.time);
                    this.gameObject.GetComponent<Pausemenu>().Pause();
                    inSettings = false;
                    settingsMenu.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    changeState(2);
                }
                break;

                //Changes the slider position
            case 2:
                volumeButton.Select();

                if (Input.GetKeyDown(KeyCode.S))
                {
                    changeState(1);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    if (slider.value >= -70)
                    {
                        slider.value -= 10;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (slider.value <= -10)
                    {
                        slider.value += 10;
                    }
                }
                break;

            default:
                break;
        }
    }

    public void startSettings()
    {
        settingsMenu.SetActive(true);
        inSettings = true;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    private void changeState(int newState)
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        state = newState;
    }
}
