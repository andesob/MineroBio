using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[] isFull;
    public GameObject[] slots;
    private bool inventoryOpen;

    public GameObject inventoryUI;


    private void Start()
    {
        inventoryOpen = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!inventoryOpen)
            {
                Debug.Log("HORE");
                InventoryOpen();
            }
            else if (inventoryOpen)
            {
                Debug.Log("NULL HORE");
                InventoryClose();
            }
        }
    }

    private void InventoryOpen()
    {
        inventoryUI.SetActive(true);
        inventoryOpen = true;
    }

    private void InventoryClose()
    {
        inventoryUI.SetActive(false);
        inventoryOpen = false;
    }
}
