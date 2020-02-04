using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{


    //stats - ARS
    public int currentHealth;
    public int maxHealth = 6;

    // Start is called before the first frame update
    void start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        handleHealth();
    }

    private void die()
    {
        //restart the game in the active scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void handleHealth()
    {

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            die();
        }
    }

}
