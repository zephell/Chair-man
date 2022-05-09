using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static event Action<int> OnEquippedSkinUpdated;
    public static event Action<string> OnBoughtSkinsUpdated;

    string boughtSkins;
    string previousBoughtSkins;

    int equippedSkin;
    int previousEquippedSkin;

    private void Awake()
    {
        boughtSkins = PlayerPrefs.GetString("Bought_Skins_Id");
        equippedSkin = PlayerPrefs.GetInt("Equipped_Skin_Id");

        OnBoughtSkinsUpdated?.Invoke(boughtSkins);
        OnEquippedSkinUpdated?.Invoke(equippedSkin);
    }

    void FixedUpdate()
    {
        //Call OnBought event when bought skins are updated
        boughtSkins = PlayerPrefs.GetString("Bought_Skins_Id");
        if (boughtSkins != previousBoughtSkins)
        {
            OnBoughtSkinsUpdated?.Invoke(boughtSkins);
        }
        previousBoughtSkins = boughtSkins;

        //Call OnEquipped event when equipped skin is switched
        equippedSkin = PlayerPrefs.GetInt("Equipped_Skin_Id");
        if (equippedSkin != previousEquippedSkin)
        {
            OnEquippedSkinUpdated?.Invoke(equippedSkin);
        }
        previousEquippedSkin = equippedSkin;
    }
}
