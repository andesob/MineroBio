using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneySystem
{
    private static int money;

    public static bool BuyItem(int cost)
    {
        if (money - cost >= 0)
        {
            money -= cost;
            return true;
        }
        return false;
    }

    public static int GetMoney()
    {
        return money;
    }

    public static void AddMoney(int amount)
    {
        money += amount;
    }
}
