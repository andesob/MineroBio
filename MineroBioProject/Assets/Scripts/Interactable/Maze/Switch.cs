using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script used on the switch to open and close the doors when a switch is triggered
 */
public class Switch : MonoBehaviour
{
    [SerializeField] GameObject switchOn;
    [SerializeField] GameObject switchOff;

    [SerializeField] GameObject[] doors;

    public bool isOn;

    private void Start()
    {
        //sets switch to off on start
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
        isOn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when the player enters the triggerbox of the switch it either turns the switch on or off and changes the sprite.
        if (collision.tag == "Player")
        {
            if (isOn)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;

                //changes state for every door in the array
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].GetComponent<doorScript>().changeState();
                }
                isOn = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;

                //changes state for every door in the array
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].GetComponent<doorScript>().changeState();
                }
                isOn = true;
            }
        }
    }

    public bool getIsOn()
    {
        return isOn;
    }
}
