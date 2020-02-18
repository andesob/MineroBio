using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{
    private const int PISTOL_INDEX = 1;
    private const int SHOTGUN_INDEX = 2;

    //public Transform gun;
    //private bool gunPickedUp;
    private bool pistolPickedUp;
    private bool shotgunPickedUp;

    private GameObject pistol;
    private GameObject shotgun;

    private shooting shootingScript;

    private void Start()
    {
        shootingScript = this.gameObject.GetComponent<shooting>();
        pistol = this.gameObject.transform.GetChild(PISTOL_INDEX).gameObject;
        shotgun = this.gameObject.transform.GetChild(SHOTGUN_INDEX).gameObject;
    }

    public void PickUpWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "PistolPickup":
                pistol.SetActive(true);
                shotgun.SetActive(false);
                pistolPickedUp = true;
                shootingScript.setGun(pistol);
                break;

            case "ShotgunPickup":
                shotgun.SetActive(true);
                pistol.SetActive(false);
                shotgunPickedUp = true;
                shootingScript.setGun(shotgun);
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
            collision.gameObject.SetActive(false);
            PickUpWeapon(collision.gameObject.name);
        }
    }
}
