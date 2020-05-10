using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[] isFull;
    public GameObject[] itemSlots;
    public Sprite pistolImage;
    public Sprite sniperImage;

    private Inventory()
    {
    }

    public void AddPicture(string weapon)
    {
        if (weapon == "PistolPickup")
        {
            this.itemSlots[0].GetComponent<Image>().sprite = pistolImage;
            Debug.Log("REEEE");
        }
        else if (weapon == "SniperPickup")
        {
            this.itemSlots[1].GetComponent<Image>().sprite = sniperImage;
        }
    }
}
