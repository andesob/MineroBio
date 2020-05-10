using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private PlayerPickupScript playerPickupScript;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPickupScript = player.GetComponent<PlayerPickupScript>();
        inventory = player.GetComponent<Inventory>();
        waitSetup();
        SetupWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupWeapons()
    {
        if (gameManager.HasSniper())
        {
            playerPickupScript.PickUpWeapon("SniperPickup");
            inventory.AddPicture("SniperPickup");
        }

        if (gameManager.HasPistol())
        {
            playerPickupScript.PickUpWeapon("PistolPickup");
            inventory.AddPicture("PistolPickup");
        }
    }

    IEnumerator waitSetup()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
