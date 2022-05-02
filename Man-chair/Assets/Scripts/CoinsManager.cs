using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private PlayerController player;

    [Header("Skins")]
    [SerializeField] private Mesh[] skins;

    private int coins;

    public int idSelectedSkin;


    private void Start()
    {
        idSelectedSkin = PlayerPrefs.GetInt("Selected Skin");
        player.SelectSkin(skins[idSelectedSkin]);
        coins = PlayerPrefs.GetInt("Coins");
        coinsText.text = coins.ToString();
    }

    public void BuySkin(int skinId)
    {
        idSelectedSkin = skinId;
        PlayerPrefs.SetInt("Selected Skin", skinId);
        player.SelectSkin(skins[idSelectedSkin]);
    }

    public void UpdateCoins()
    {
        coins = PlayerPrefs.GetInt("Coins");
        coinsText.text = coins.ToString();
    }
}