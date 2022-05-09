using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

enum SkinState
{
    NotPurchased,
    Bought,
    Equipped
}

public class SkinItem : MonoBehaviour
{
    public SkinProps skinProps;

    public TMP_Text skinName;
    public TMP_Text cost;
    public Image image;
    public GameObject buyButton;
    public GameObject equipButton;
    public GameObject equipped;

    private SkinState skinState = SkinState.NotPurchased;
    private SkinState previousSkinState;

    private void Awake()
    {
        //PlayerPrefs.SetString("Bought_Skins_Id", "");
        PlayerPrefs.Save();
        skinName.text = skinProps.name;
        cost.text = skinProps.cost.ToString();
        image.sprite = skinProps.icon;

        if (PlayerPrefs.GetInt("Equipped_Skin_Id") == skinProps.id)
            skinState = SkinState.Equipped;
        else if (PlayerPrefs.GetString("Bought_Skins_Id").Contains(skinProps.id.ToString()))
            skinState = SkinState.Bought;

        EventsManager.OnEquippedSkinUpdated += OnEquippedSkinUpdated;
        EventsManager.OnBoughtSkinsUpdated += OnBoughtSkinsUpdated;
    }

    private void OnDisable()
    {
        EventsManager.OnEquippedSkinUpdated -= OnEquippedSkinUpdated;
        EventsManager.OnBoughtSkinsUpdated -= OnBoughtSkinsUpdated;
    }

    public void OnBoughtSkinsUpdated(string obj)
    {
        if(!obj.Contains(skinProps.id.ToString()))
        {
            skinState = SkinState.NotPurchased;
        }
        else
        {
            skinState = SkinState.Bought;
        }
    }

    public void OnEquippedSkinUpdated(int id)
    {
        if (PlayerPrefs.GetString("Bought_Skins_Id").Contains(skinProps.id.ToString()))
        {
            skinState = (id == skinProps.id) ? SkinState.Equipped : SkinState.Bought;
        }
    }

    private void FixedUpdate()
    {
        //Executes when skin state is changed
        if (previousSkinState != skinState)
        {
            //Change buy button to equip or equip button to equipped
            SwitchUISkinState(skinState);
        }
        previousSkinState = skinState;
    }

    public void OnPressBuyButton()
    {
        //Check if player has enough money to buy skin
        if (PlayerPrefs.GetInt("Coins") - skinProps.cost < 0) return;

        CoinsData.DecreaseCoinsCount(skinProps.cost);
        string boughtSkinsId = PlayerPrefs.GetString("Bought_Skins_Id");
        PlayerPrefs.SetString("Bought_Skins_Id", $"{boughtSkinsId}, {skinProps.id}");
        PlayerPrefs.Save();

        skinState = SkinState.Bought;
    }

    public void OnPressEquipButton()
    {
        PlayerPrefs.SetInt("Equipped_Skin_Id", skinProps.id);
        PlayerPrefs.Save();

        skinState = SkinState.Equipped;
    }

    private void SwitchUISkinState(SkinState state)
    {
        buyButton.SetActive(false);
        equipButton.SetActive(false);
        equipped.SetActive(false);

        switch (state)
        {
            case SkinState.NotPurchased:

                buyButton.SetActive(true);
                break;

            case SkinState.Bought:

                equipButton.SetActive(true);
                break;

            case SkinState.Equipped:

                equipped.SetActive(true);
                break;
        }
    }
}
