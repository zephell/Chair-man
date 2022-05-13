using UnityEngine;

public class CoinsData
{
    public delegate void OnCoinsChanged(int coins);
    public static event OnCoinsChanged OnCoinsValueChanged;

    public static void IncreaseCoinsCount(int amount)
    {
        int coinsCount = PlayerPrefs.GetInt("Coins") + amount;
        UpdateCoins(coinsCount);
    }
    
    public static void DecreaseCoinsCount(int amount)
    {
        int coinsCount = PlayerPrefs.GetInt("Coins") - amount;
        UpdateCoins(coinsCount);
    }

    private static void UpdateCoins(int amount)
    {
        PlayerPrefs.SetInt("Coins", amount);
        PlayerPrefs.Save();

        OnCoinsValueChanged?.Invoke(PlayerPrefs.GetInt("Coins"));
    }
}
