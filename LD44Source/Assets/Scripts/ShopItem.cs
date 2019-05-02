using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu]
public class ShopItem: ScriptableObject
{
    public GameObject item;
    public string itemName;
    public string itemDesc;
    public int itemCost;

    private Sprite thumbnail;

    public Sprite getThumbnail()
    {
        return item.GetComponent<SpriteRenderer>().sprite;
    }

    public string ToString()
    {
        return itemName + ": " + itemCost;
    }
}
