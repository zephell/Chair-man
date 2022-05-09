using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinsData
{
    public static void IncreaseCoinsCount(int amount)
    {
        int coinsCount = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", coinsCount + amount);
        PlayerPrefs.Save();
    }
    
    public static void DecreaseCoinsCount(int amount)
    {
        int coinsCount = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", coinsCount - amount);
        PlayerPrefs.Save();
    }
}
