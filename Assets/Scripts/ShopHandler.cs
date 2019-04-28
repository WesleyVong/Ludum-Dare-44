using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    public UIVariables UIVar;
    public GameObject player;
    public ShopItem[] Items;
    public GameObject[] StoreSlots = new GameObject[10];

    public AudioSource audio;

    public void Start()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
            {
                StoreSlots[i].GetComponentInChildren<Text>().text = Items[i].ToString();
                StoreSlots[i].GetComponentsInChildren<Image>()[1].sprite = Items[i].thumbnail;
            }
        }
    }

    public void Purchase(int itemNum)
    {
        for (int i = 0; i < player.GetComponent<PlayerControls>().Inventory.Length; i++)
        {
            if (player.GetComponent<PlayerControls>().Inventory[i] == null)
            {
                GameObject obj = Instantiate(Items[itemNum].item, player.transform);
                obj.SetActive(false);
                player.GetComponent<PlayerControls>().Inventory[i] = obj;
                UIVar.UIs[0].SetValue((int.Parse(UIVar.UIs[0].GetValue()) - Items[itemNum].itemCost).ToString());
                audio.Play();
                break;
            }
        }
    }
}
