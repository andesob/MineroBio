﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Script that sets up each scene
 * Gets information from the gamemanager script and sets up the seen accordingly
 */
public class SceneSetup : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private PlayerPickupScript playerPickupScript;
    private Inventory inventory;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPickupScript = player.GetComponent<PlayerPickupScript>();
        inventory = player.GetComponent<Inventory>();
    }

    void Start()
    {
        SetupWeapons();
        SetPlayerHealth();
        SetupLevelHub();
    }

    private void SetupWeapons()
    {
        if (gameManager.HasSniper())
        {
            playerPickupScript.PickUpWeapon("SniperPickup");
            inventory.AddWeapon("SniperPickup");
        }

        if (gameManager.HasPistol())
        {
            playerPickupScript.PickUpWeapon("PistolPickup");
            inventory.AddWeapon("PistolPickup");
        }
    }

    private void SetPlayerHealth()
    {
        int health = gameManager.GetHealth();
        int maxHealth = 12;
        HeartsHealthVisual2.heartsHealthSystemStatic.Damage(maxHealth - health);
    }

    //Removes blockades from the levelhub if a level is finished
    private void SetupLevelHub()
    {
        if(SceneManager.GetActiveScene().name == "levelHubScene")
        {
            if (gameManager.IsLevel1Finished())
            {
                GameObject.Find("Blockades/blockade1").SetActive(false);
            }

            if (gameManager.IsLevel2Finished())
            {
                GameObject.Find("Blockades/blockade2").SetActive(false);
            }

            if (gameManager.IsLevel3Finished())
            {
                GameObject.Find("Blockades/blockade3").SetActive(false);
            }
        }
    }
}
