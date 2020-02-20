using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int PISTOL_INDEX = 1;
    private const int SHOTGUN_INDEX = 2;
    private const int SNIPER_INDEX = 3;

    public static bool isGamePaused;
    private static bool isMoneyMade;

    private GameObject player;
    private GameObject pistol;
    private GameObject shotgun;
    private GameObject sniper;

    private shooting shootingScript;
    private PlayerPickupScript playerPickupScript;

    private bool Nr1Pressed;
    private bool Nr2Pressed;
    private bool Nr3Pressed;


    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;
        player = this.gameObject;
        pistol = player.transform.GetChild(PISTOL_INDEX).gameObject;
        shotgun = player.transform.GetChild(SHOTGUN_INDEX).gameObject;
        sniper = player.transform.GetChild(SNIPER_INDEX).gameObject;
        shootingScript = player.GetComponent<shooting>();
        playerPickupScript = player.GetComponent<PlayerPickupScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Damage(1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddMoney();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Nr1Pressed = true;
        }
        else
        {
            Nr1Pressed = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Nr2Pressed = true;
        }
        else
        {
            Nr2Pressed = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Nr3Pressed = true;
        }
        else
        {
            Nr3Pressed = false;
        }
        chooseWeapon();
    }


    public void Damage(int damageAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Damage(damageAmount);
    }

    public void Heal(int damageAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Heal(damageAmount);
    }

    public void AddMoney()
    {
        MoneySystem.AddMoney(20);
    }


    private void chooseWeapon()
    {
        if (Nr1Pressed && playerPickupScript.hasPistol())
        {
            pistol.SetActive(true);
            shotgun.SetActive(false);
            sniper.SetActive(false);
            shootingScript.setGun(pistol);
            
        }

        if (Nr2Pressed && playerPickupScript.hasShotgun())
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            sniper.SetActive(false);
            shootingScript.setGun(shotgun);
        }

        if(Nr3Pressed && playerPickupScript.hasSniper())
        {
            pistol.SetActive(false);
            shotgun.SetActive(false);
            sniper.SetActive(true);
            shootingScript.setGun(sniper);
            
        }
    }

    public void rotateGun(string direction)
    {
        if (playerPickupScript.hasGun())
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            Transform weapon = shootingScript.getGun().transform;
          
            switch (direction)
            {
                case "W":
                   
                  //  firepoint.rotation = Quaternion.Euler(0f, 0f, 90f);
                  //firepoint.position = new Vector3(playerX + 0.2f, playerY, 0f);
                    break;

                case "S":
                   // firePoint.rotation = Quaternion.Euler(0f, 0f, 270f);
                   // firepoint.position = new Vector3(playerX - 0.25f, playerY, 0f);
                    break;

                case "A":
                  //  weapon.rotation = Quaternion.Euler(0f, 180f, 0f);
                 //   firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                  //  firepoint.position = new Vector3(playerX - 0.1f, playerY - 0.16f, 0f);
                    break;

                case "D":
                  
                    //weapon.rotation = Quaternion.Euler(0f, 0f, 0f);
                    //firepoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                 //   firepoint.position = new Vector3(playerX + 0.1f, playerY - 0.13f, 0f);
                    break;
            }
        }
        
    }
    
}
