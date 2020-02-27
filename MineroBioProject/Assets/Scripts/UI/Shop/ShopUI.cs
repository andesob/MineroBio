using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItem(Item.GetSprite(Item.ItemType.Pistol), "Pistol", Item.GetCost(Item.ItemType.Pistol), 0);
    }

    private void CreateItem(Sprite itemSprite, string itemName, int itemCost, int index)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * index);

        shopItemTransform.Find("itemName").GetComponent<Text>().text = itemName;
        shopItemTransform.Find("CostText").GetComponent<Text>().text = itemCost.ToString();
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
    }
}
