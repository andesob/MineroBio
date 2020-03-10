using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{
    private const int PISTOL_INDEX = 1;
    private const int SHOTGUN_INDEX = 2;
    private const int SNIPER_INDEX = 3;

    //public Transform gun;
    //private bool gunPickedUp;
    private bool pistolPickedUp;
    private bool shotgunPickedUp;
    private bool sniperPickedUp;

    private GameObject pistol;
    private GameObject shotgun;
    private GameObject sniper;

    private shooting shootingScript;

    private void Start()
    {
        shootingScript = this.gameObject.GetComponent<shooting>();
        pistol = this.gameObject.transform.GetChild(PISTOL_INDEX).gameObject;
        shotgun = this.gameObject.transform.GetChild(SHOTGUN_INDEX).gameObject;
        sniper = this.gameObject.transform.GetChild(SNIPER_INDEX).gameObject;
    }

    public void PickUpWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "PistolPickup":
                pistol.SetActive(true);
                shotgun.SetActive(false);
                sniper.SetActive(false);
                pistolPickedUp = true;
                shootingScript.setGun(pistol);
                
                
                break;

            case "ShotgunPickup":
                shotgun.SetActive(true);
                pistol.SetActive(false);
                sniper.SetActive(false);
                shotgunPickedUp = true;
                shootingScript.setGun(shotgun);
                break;
            
            case "SniperPickup":
                sniper.SetActive(true);
                Debug.Log(sniper.name);
                pistol.SetActive(false);
                shotgun.SetActive(false);
                sniperPickedUp = true;
                shootingScript.setGun(sniper);
                
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

    public bool hasGun()
    {
        if(shotgunPickedUp || pistolPickedUp)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            //collision.gameObject.SetActive(false);
            PickUpWeapon(collision.gameObject.name);
        }
    }
}
