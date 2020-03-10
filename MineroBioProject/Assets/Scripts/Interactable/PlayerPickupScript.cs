using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{
    private const int PISTOL_INDEX = 0;
    private const int SHOTGUN_INDEX = 1;
    private const int SNIPER_INDEX = 2;

    //public Transform gun;
    //private bool gunPickedUp;
    private bool pistolPickedUp;
    private bool shotgunPickedUp;
    private bool sniperPickedUp;

    private GameObject pistol;
    private GameObject shotgun;
    private GameObject sniper;

    private shooting shootingScript;
    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = this.gameObject.GetComponent<PlayerController>();
        shootingScript = this.gameObject.GetComponent<shooting>();
        GameObject weapons = this.gameObject.transform.GetChild(0).gameObject;
        pistol = weapons.transform.GetChild(PISTOL_INDEX).gameObject;
        shotgun = weapons.transform.GetChild(SHOTGUN_INDEX).gameObject;
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

            case "ShotgunPickup":
                shotgunPickedUp = true;
                shootingScript.SetGun(shotgun);
                playerControllerScript.AddWeapon(shotgun);
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

    public bool hasShotgun()
    {
        return shotgunPickedUp;
    }

    public bool hasSniper()
    {
        return sniperPickedUp;
    }

    public bool hasWeapon()
    {
        if(shotgunPickedUp || pistolPickedUp || sniperPickedUp)
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
