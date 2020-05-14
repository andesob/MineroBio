using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Script used to initialize the math quiz on collision with the player.
 */
public class StartMathQuiz : MonoBehaviour
{
    public GameObject MathUIObject;

    private doorScript doorScript;
    private bool haveAnsweredQuiz = false;

    private void Start()
    {
        doorScript = this.gameObject.GetComponent<doorScript>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && collider.isTrigger && !haveAnsweredQuiz)
        {
            if (collider != null)
            {
                SetMathUIStatusToActive();
                doorScript.OpenDoor();
                haveAnsweredQuiz = true;
            }
        }
    }

    // Sets the MathUI object to active, and pauses the game.
    public void SetMathUIStatusToActive()
    {
        MathUIObject.SetActive(true);
    }
}
