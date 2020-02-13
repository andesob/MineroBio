using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int PISTOL_INDEX = 1;
    private const int SHOTGUN_INDEX = 2;

    private GameObject player;
    private shooting shootingScript;
    private PlayerPickupScript playerPickupScript;
    

    private bool Nr1Pressed;
    private bool Nr2Pressed;


    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
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


    private void chooseWeapon()
    {
        if (Nr1Pressed && playerPickupScript.hasPistol())
        {
            this.gameObject.transform.GetChild(PISTOL_INDEX).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(SHOTGUN_INDEX).gameObject.SetActive(false);
            shootingScript.setGun(this.gameObject.transform.GetChild(PISTOL_INDEX));
        }
        
        if (Nr2Pressed && playerPickupScript.hasShotgun())
        {
            this.gameObject.transform.GetChild(SHOTGUN_INDEX).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(PISTOL_INDEX).gameObject.SetActive(false);
            shootingScript.setGun(this.gameObject.transform.GetChild(SHOTGUN_INDEX));
        }
    }

    public void rotateGun(string direction)
    {
        if (playerPickupScript.hasGun())
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;

            switch (direction)
            {
                case "W":
                    shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                    shootingScript.firePoint.transform.position = new Vector3(playerX + 0.2f, playerY, 0f);
                    break;

                case "S":
                    shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                    shootingScript.firePoint.transform.position = new Vector3(playerX - 0.25f, playerY, 0f);
                    break;

                case "A":
                    shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, 90f);
                    shootingScript.getGun().rotation = Quaternion.Euler(0f, 180f, 0f);
                    shootingScript.firePoint.transform.position = new Vector3(playerX - 0.1f, playerY - 0.16f, 0f);
                    break;

                case "D":
                    shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, -90f);
                    shootingScript.getGun().rotation = Quaternion.Euler(0f, 0f, 0f);
                    shootingScript.firePoint.transform.position = new Vector3(playerX + 0.1f, playerY - 0.13f, 0f);
                    break;
            }
        }
    }
}
