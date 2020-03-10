using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Sprite itemImage;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            Debug.Log("HERE");
            for (int i = 0; i < inventory.itemSlots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Debug.Log("item picked up");
                    inventory.itemSlots[i].GetComponent<Image>().sprite = itemImage;
                    break;
                }
            }
        }
    }
}
