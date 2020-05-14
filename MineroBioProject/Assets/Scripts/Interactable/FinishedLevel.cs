using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script updates the gamemanager script when each level is finished
 */
public class FinishedLevel : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                gameManager.FinishedLevel1();
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                gameManager.FinishedLevel2();
            }
            else if (SceneManager.GetActiveScene().name == "Level3")
            {
                gameManager.FinishedLevel3();
            }
        }
    }
}
