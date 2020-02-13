using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{
    private const int PISTOL_INDEX = 1;
    private const int SHOTGUN_INDEX = 2;

    //public Transform gun;
    private bool gunPickedUp;
    private bool pistolPickedUp;
    private bool shotgunPickedUp;

    private shooting shootingScript;

    private void Start()
    {
        shootingScript = this.gameObject.GetComponent<shooting>();
    }

    public void PickUpWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "PistolPickup":
                this.gameObject.transform.GetChild(PISTOL_INDEX).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(SHOTGUN_INDEX).gameObject.SetActive(false);
                pistolPickedUp = true;
                shootingScript.setGun(this.gameObject.transform.GetChild(PISTOL_INDEX));
                break;

            case "ShotgunPickup":
                this.gameObject.transform.GetChild(SHOTGUN_INDEX).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(PISTOL_INDEX).gameObject.SetActive(false);
                shotgunPickedUp = true;
                shootingScript.setGun(this.gameObject.transform.GetChild(SHOTGUN_INDEX));
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
