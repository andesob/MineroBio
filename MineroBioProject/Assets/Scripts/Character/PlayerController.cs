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

    private bool canMove = true;
    private float knockbackTime;

    private GameObject player;
    private GameObject pistol;
    private GameObject shotgun;
    private GameObject sniper;

    private shooting shootingScript;
    private PlayerPickupScript playerPickupScript;
    private Rigidbody2D playerRigidbody2D;

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
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            //Damage(1);
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
        ChooseWeapon();
    }

    // Damages the player and knock it baclward. 
    public void Damage(Vector2 thrust, int damageAmount)
    {
        playerRigidbody2D.AddForce(thrust, ForceMode2D.Impulse);
        HeartsHealthVisual2.heartsHealthSystemStatic.Damage(damageAmount);
        StartCoroutine(KnockbackTimer(playerRigidbody2D));
       
    }

    // A timer for how long the player should be knocked back when taking damage.
    private IEnumerator KnockbackTimer(Rigidbody2D thisRigidbody2D)
    {
        canMove = false;
        yield return new WaitForSeconds(knockbackTime);
        thisRigidbody2D.velocity = Vector2.zero;
        canMove = true;

    }
    // Returns the players position
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Heals the player
    public void Heal(int damageAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Heal(damageAmount);
    }

    public void AddMoney()
    {
        MoneySystem.AddMoney(20);
    }


    private void ChooseWeapon()
    {
        if (Nr1Pressed && playerPickupScript.hasPistol())
        {
            pistol.SetActive(true);
            shotgun.SetActive(false);
            sniper.SetActive(false);
            shootingScript.SetGun(pistol);
            
        }

        if (Nr2Pressed && playerPickupScript.hasShotgun())
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            sniper.SetActive(false);
            shootingScript.SetGun(shotgun);
        }

        if(Nr3Pressed && playerPickupScript.hasSniper())
        {
            pistol.SetActive(false);
            shotgun.SetActive(false);
            sniper.SetActive(true);
            shootingScript.SetGun(sniper);
            
        }
    }

    public void RotateGun(string direction)
    {
        if (playerPickupScript.hasGun())
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            Transform weapon = shootingScript.GetGun().transform;
          
            switch (direction)
            {
                case "W":
                   
                  //firepoint.rotation = Quaternion.Euler(0f, 0f, 90f);
                  //firepoint.position = new Vector3(playerX + 0.2f, playerY, 0f);
                    break;

                case "S":
                   // firePoint.rotation = Quaternion.Euler(0f, 0f, 270f);
                   // firepoint.position = new Vector3(playerX - 0.25f, playerY, 0f);
                    break;

                case "A":
                    weapon.rotation = Quaternion.Euler(0f, 180f, 0f);
                 //   firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                  //  firepoint.position = new Vector3(playerX - 0.1f, playerY - 0.16f, 0f);
                    break;

                case "D":
                  
                    weapon.rotation = Quaternion.Euler(0f, 0f, 0f);
                    //firepoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                 //   firepoint.position = new Vector3(playerX + 0.1f, playerY - 0.13f, 0f);
                    break;
            }
        }
        
    }

    public bool CanMove()
    {
        return canMove;
    }
    
}
