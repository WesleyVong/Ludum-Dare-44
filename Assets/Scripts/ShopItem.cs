using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public GameObject item;
    public Sprite thumbnail;
    public string itemName;
    public int itemCost;

    public string ToString()
    {
        return itemName + ": " + itemCost;
    }
}
