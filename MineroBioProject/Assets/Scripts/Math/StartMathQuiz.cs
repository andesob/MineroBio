using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMathQuiz : MonoBehaviour
{
    public GameObject MathUIObject;
    private bool haveAnsweredQuiz = false;
    private doorScript doorScript; 

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
    // TODO Might want to move this method. Depends on the object that calls it.
    public void SetMathUIStatusToActive()
    {

        MathUIObject.SetActive(true);
        
    }
}
