using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Sprite itemImage;
    private Inventory inventory;
    private int slot;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(this.gameObject.name == "PistolPickup")
        {
            slot = 0;
        } else if(this.gameObject.name == "SniperPickup")
        {
            slot = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
                if(inventory.isFull[slot] == false)
                {
                    inventory.isFull[slot] = true;
                    inventory.AddPicture(this.gameObject.name);
            }
        }
    }
}
