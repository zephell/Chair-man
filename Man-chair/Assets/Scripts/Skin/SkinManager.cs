using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] List<SkinProps> skins = new List<SkinProps>();

    private void Awake()
    {
        GameObject obj = GetSkin(PlayerPrefs.GetInt("Equipped_Skin_Id")).skinModel;
        GameObject newObj = Instantiate(obj, transform.position, transform.rotation);
        newObj.transform.parent = transform;

        EventsManager.OnEquippedSkinUpdated += OnEquippedSkinUpdated;
    }

    private void OnDisable()
    {
        EventsManager.OnEquippedSkinUpdated -= OnEquippedSkinUpdated;
    }

    public void OnEquippedSkinUpdated(int id)
    {
        GameObject obj = GetSkin(id).skinModel;
        if (obj != null)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            GameObject newObj = Instantiate(obj, transform.position, transform.rotation);
            newObj.transform.parent = transform;
        }
    }

    private SkinProps GetSkin(int id)
    {
        for (int i = 0; i < skins.Count; i++)
        {
            if (skins[i].id == id)
            {
                return skins[i];
            }
        }

        Debug.LogWarning($"Skin with id: {id} not defined in skin managers skin");
        return null;
    }
}
