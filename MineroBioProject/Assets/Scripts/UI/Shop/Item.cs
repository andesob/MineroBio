using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Pistol,
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Pistol:
                return 30;
            default:
                return 0;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Pistol:
                return GameAssets.i.Pistol;
        }
    }
}
