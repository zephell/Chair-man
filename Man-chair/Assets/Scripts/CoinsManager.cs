using UnityEngine;
using TMPro;

public  class CoinsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

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

    private void UpdateCoinsUI()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}