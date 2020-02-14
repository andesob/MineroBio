using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int PISTOL_INDEX = 1;
    private const int SHOTGUN_INDEX = 2;

    public static bool isGamePaused;

    private GameObject player;
    private GameObject pistol;
    private GameObject shotgun;

    private shooting shootingScript;
    private PlayerPickupScript playerPickupScript;

    private bool Nr1Pressed;
    private bool Nr2Pressed;


    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;
        player = this.gameObject;
        pistol = player.transform.GetChild(PISTOL_INDEX).gameObject;
        shotgun = player.transform.GetChild(SHOTGUN_INDEX).gameObject;
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
            pistol.SetActive(true);
            shotgun.SetActive(false);
            shootingScript.setGun(pistol);
        }
        
        if (Nr2Pressed && playerPickupScript.hasShotgun())
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            shootingScript.setGun(shotgun);
        }
    }

    public void rotateGun(string direction)
    {
        if (playerPickupScript.hasGun())
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            Transform firepoint = shootingScript.firePoint;
            Transform weapon = shootingScript.getGun().transform;

            switch (direction)
            {
                case "W":
                    firepoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                    firepoint.position = new Vector3(playerX + 0.2f, playerY, 0f);
                    break;

                case "S":
                    firepoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                    firepoint.position = new Vector3(playerX - 0.25f, playerY, 0f);
                    break;

                case "A":
                    weapon.rotation = Quaternion.Euler(0f, 180f, 0f);
                    firepoint.rotation = Quaternion.Euler(0f, 0f, 90f);
                    firepoint.position = new Vector3(playerX - 0.1f, playerY - 0.16f, 0f);
                    break;

                case "D":
                    weapon.rotation = Quaternion.Euler(0f, 0f, 0f);
                    firepoint.rotation = Quaternion.Euler(0f, 0f, -90f);
                    firepoint.position = new Vector3(playerX + 0.1f, playerY - 0.13f, 0f);
                    break;
            }
        }
    }
}
