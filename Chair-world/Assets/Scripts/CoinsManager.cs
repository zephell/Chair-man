using UnityEngine;
using TMPro;

public  class CoinsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    private void Start() => CoinsData.OnCoinsValueChanged += UpdateCoinsUI;

    private void UpdateCoinsUI(int coins) => coinsText.text = coins.ToString();
}