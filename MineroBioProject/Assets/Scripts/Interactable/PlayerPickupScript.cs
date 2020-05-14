using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for when the player picks up a weapon from the ground
 */
public class PlayerPickupScript : MonoBehaviour
{
    private const int PISTOL_INDEX = 0;
    private const int SNIPER_INDEX = 1;

    private bool pistolPickedUp;
    private bool sniperPickedUp;

    private GameObject pistol;
    private GameObject sniper;

    private shooting shootingScript;
    private PlayerController playerControllerScript;

    private void Awake()
    {
        playerControllerScript = this.gameObject.GetComponent<PlayerController>();
        shootingScript = this.gameObject.GetComponent<shooting>();

        GameObject weapons = this.gameObject.transform.GetChild(0).gameObject;
        pistol = weapons.transform.GetChild(PISTOL_INDEX).gameObject;
        sniper = weapons.transform.GetChild(SNIPER_INDEX).gameObject;
    }

    public void PickUpWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "PistolPickup":
                pistolPickedUp = true;
                shootingScript.SetGun(pistol);
                playerControllerScript.AddWeapon(pistol);
                break;
            
            case "SniperPickup":
                sniperPickedUp = true;
                shootingScript.SetGun(sniper);
                playerControllerScript.AddWeapon(sniper);
                break;
        }    
    }

    public bool hasPistol()
    {
        return pistolPickedUp;
    }

    public bool hasSniper()
    {
        return sniperPickedUp;
    }

    public bool hasWeapon()
    {
        if(pistolPickedUp || sniperPickedUp)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            PickUpWeapon(collision.gameObject.name);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            MoneySystem.AddMoney(1);
        }
    }
}
