using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] KeyCode increaseCoinsKey = KeyCode.C;
    [SerializeField] int increaseAmount = 50;

    [SerializeField] KeyCode decreaseCoinsKey = KeyCode.V;
    [SerializeField] int decreaseAmount = 50;

    [SerializeField] KeyCode resetBoughtSkinsKey = KeyCode.B;
    [SerializeField] private KeyCode _resetAllPlayerPrefs = KeyCode.R;

    private void Update()
    {
        if (Input.GetKeyDown(increaseCoinsKey))
            CoinsData.IncreaseCoinsCount(increaseAmount);

        if (Input.GetKeyDown(decreaseCoinsKey))
            CoinsData.DecreaseCoinsCount(decreaseAmount);

        if (Input.GetKeyDown(resetBoughtSkinsKey))
        {
            PlayerPrefs.SetString("Bought_Skins_Id", "");
            PlayerPrefs.Save();
        }

        if(Input.GetKeyDown(_resetAllPlayerPrefs))
            PlayerPrefs.DeleteAll();
    }
}
