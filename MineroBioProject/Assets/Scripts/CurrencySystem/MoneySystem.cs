using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private static MoneySystem instance;

    private int money;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "MoneySystem";
        instance = this;
        AddMoney(0);
    }

    public static bool BuyItem(int cost)
    {
        if(instance.money - cost >= 0)
        {
            instance.money -= cost;
            return true;
        }
        return false;
    }

    public static int GetMoney()
    {
        return instance.money;
    }

    public static void AddMoney(int amount)
    {
        instance.money += amount;
    }
}
