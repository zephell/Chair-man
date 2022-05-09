using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Skin", menuName = "ScriptableObjects/Skin")]
public class SkinProps : ScriptableObject
{
    public new string name;
    public int id;
    public int cost;
    public Sprite icon;
    public GameObject skinModel;
}
