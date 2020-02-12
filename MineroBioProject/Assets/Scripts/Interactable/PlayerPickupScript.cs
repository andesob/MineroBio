using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{

    public Transform gun;
    private bool gunPickedUp;

    public void PickUpGun()
    {
        gun.gameObject.SetActive(true);
        gunPickedUp = true;
    }

    public bool hasGun()
    {
        return gunPickedUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            PickUpGun();
        }
    }

}
