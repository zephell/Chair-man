using UnityEngine;
using TMPro;

public  class CoinsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private PlayerController player;

    [Header("Skins")]
    [SerializeField] private GameObject[] skins;

    private int previousCoinsCount;

    private void FixedUpdate()
    {
        int coinsCount = PlayerPrefs.GetInt("Coins");
        if (coinsCount != previousCoinsCount)
        {
            //udate coins ui when coins count are changed
            UpdateCoinsUI();
        }
        previousCoinsCount = coinsCount;
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Coins", 100);
        PlayerPrefs.Save();
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}