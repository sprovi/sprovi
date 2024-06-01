using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemData
{
    public ItemType ItemType;
    public Sprite Sprite;
}

[CreateAssetMenu]
public class ItemIcons : ScriptableObject
{
    public ItemData[] ItemDatas;
    public Sprite GetSprite(ItemType itemType)
    {
        for (int i = 0; i < ItemDatas.Length; i++)
        {
            if (ItemDatas[i].ItemType == itemType)
            {
                return ItemDatas[i].Sprite;
            }
        }
        return null;
    }
}
