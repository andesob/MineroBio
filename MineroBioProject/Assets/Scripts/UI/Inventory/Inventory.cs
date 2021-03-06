﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script used to manage and keep control of the playerinventory
 */
public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[] isFull;
    public GameObject[] itemSlots;
    public Sprite pistolImage;
    public Sprite sniperImage;

    public void AddWeapon(string weapon)
    {
        if (weapon == "PistolPickup")
        {
            GameObject.Find("Canvas/InventoryUI/InventoryGrid/Inventoryslot1").SetActive(true);
        }
        else if (weapon == "SniperPickup")
        {
            GameObject.Find("Canvas/InventoryUI/InventoryGrid/Inventoryslot2").SetActive(true);
        }
    }
}
