using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool isGamePaused;

    private bool canMove = true;
    private float knockbackTime = 0.08f;

    public Animator gunAnimation;

    private GameObject player;

    private GameManager gameManager;
    private shooting shootingScript;
    private Rigidbody2D playerRigidbody2D;

    private List<GameObject> weaponList = new List<GameObject>();

    private bool Nr1Pressed;
    private bool Nr2Pressed;

    private string activeWeapon = "";

    // Start is called before the first frame update
    void Awake()
    {
        isGamePaused = false;
        player = this.gameObject;
        shootingScript = player.GetComponent<shooting>();
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
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


    // Heals the player
    public void Heal(int healingAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Heal(healingAmount);
    }

    public void AddMoney()
    {
        MoneySystem.AddMoney(5);
    }


    private void ChooseWeapon()
    {
        if (Nr1Pressed && gameManager.HasPistol())
        {
            activeWeapon = "Pistol";
        }

        if (Nr2Pressed && gameManager.HasSniper())
        {
            activeWeapon = "Sniper";
        }

        foreach (GameObject weapon in weaponList)
        {
            if (weapon.name == activeWeapon)
            {
                weapon.SetActive(true);
                shootingScript.SetGun(weapon);
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }

    public void RotateGun(string direction)
    {
        if (gameManager.HasWeapon())
        {
            float playerX = GetPosition().x;
            float playerY = GetPosition().y;
            GameObject weapon = shootingScript.GetGun();
            Transform weaponTransform = weapon.transform;

            switch (direction)
            {
                case "W":
                    if (weapon != null && weapon.name == "Sniper")
                    {
                        gunAnimation.Play("ChangeDirectionX");
                    }
                    break;

                case "S":
                    if (weapon != null && weapon.name == "Sniper")
                    {
                        gunAnimation.Play("ChangeDirectionXDown");
                    }
                    break;

                case "A":
                    weaponTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    if (weapon != null && weapon.name == "Sniper")
                    {
                        gunAnimation.Play("ChangeDirectionYLeft");
                    }
                    break;

                case "D":
                    weaponTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    if (weapon != null && weapon.name == "Sniper")
                    {
                        gunAnimation.Play("ChangeDirectionY");
                    }
                    break;
            }
        }
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void setCanMove(bool boolean)
    {
        canMove = boolean;
    }

    public void AddWeapon(GameObject weapon)
    {
        weaponList.Add(weapon);
        activeWeapon = weapon.name;
        if (activeWeapon == "Pistol")
        {
            gameManager.PickUpPistol();
        }
        else if(activeWeapon == "Sniper")
        {
            gameManager.PickUpSniper();
        }
    }

    // Returns the players position
    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
