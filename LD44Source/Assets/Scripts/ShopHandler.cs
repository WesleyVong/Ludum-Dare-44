using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    public UIVariables UIVar;
    public GameObject player;
    
    public GameObject[] StoreSlots = new GameObject[10];

    public AudioSource audio;

    private ShopItem[] Items;

    public void SetItems(ShopItem[] items)
    {
        Items = items;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
            {
                StoreSlots[i].GetComponentInChildren<Text>().text = Items[i].ToString();
                StoreSlots[i].GetComponentsInChildren<Image>()[1].sprite = Items[i].getThumbnail();
            }
        }
    }

    public ShopItem GetItem(int index)
    {
        return Items[index];
    }

    public ShopItem[] GetItems()
    {
        return Items;
    }

    public void Purchase(int itemNum)
    {
        if (int.Parse(UIVar.UIs[0].GetValue()) - Items[itemNum].itemCost >= 0){
            for (int i = 0; i < player.GetComponent<PlayerControls>().Inventory.Length; i++)
            {
                if (player.GetComponent<PlayerControls>().Inventory[i] == null)
                {
                    GameObject obj = Instantiate(Items[itemNum].item, player.transform);
                    obj.SetActive(false);
                    player.GetComponent<PlayerControls>().Inventory[i] = obj;
                    player.GetComponent<PlayerControls>().SwitchSlots();

                    UIVar.UIs[0].SetValue((int.Parse(UIVar.UIs[0].GetValue()) - Items[itemNum].itemCost).ToString());
                    audio.Play();
                    break;
                }
            }
        }
    }
}
