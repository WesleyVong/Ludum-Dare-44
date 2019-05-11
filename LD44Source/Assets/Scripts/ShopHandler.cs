using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    public UIVariables UIVar;
    [Tooltip("Index of ui values which the purchase will subtract from.")]
    public int accessIndex = 0;
    private GameObject player;
    
    public GameObject[] StoreSlots = new GameObject[10];

    public AudioSource audio;

    private ShopItem[] Items;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void SetItems(ShopItem[] items)
    {
        Items = items;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
            {
                StoreSlots[i].GetComponentInChildren<Text>().text = Items[i].ToString();

                // Make image reflect size of the sprite
                Sprite sprite = Items[i].getThumbnail();
                float spriteX = sprite.bounds.size.x;
                float spriteY = sprite.bounds.size.y;
                // Sprite size is square or is wider than it is tall
                if (spriteX >= spriteY)
                {
                    // Makes sure sprite size reaches as large as possible
                    float factor = 60 / spriteX;
                    spriteX *= factor;
                    spriteY *= factor;
                }
                else
                // Sprite size is taller that it is wide
                {
                    // Makes sure sprite size reaches as large as possible
                    float factor = 60 / spriteY;
                    spriteX *= factor;
                    spriteY *= factor;
                }
                StoreSlots[i].GetComponentsInChildren<Image>()[1].rectTransform.sizeDelta = new Vector2(spriteX, spriteY);
                StoreSlots[i].GetComponentsInChildren<Image>()[1].sprite = sprite;
            }
        }
    }

    public ShopItem GetItem(int index)
    {
        if (index > Items.Length - 1)
        {
            return new ShopItem();
        }
        return Items[index];
    }

    public ShopItem[] GetItems()
    {
        return Items;
    }

    public void Purchase(int itemNum)
    {
        if (itemNum < Items.Length)
        {
            if (int.Parse(UIVar.UIs[accessIndex].GetValue()) - Items[itemNum].itemCost >= 0)
            {
                for (int i = 0; i < player.GetComponent<PlayerControls>().Inventory.Length; i++)
                {
                    if (player.GetComponent<PlayerControls>().Inventory[i] == null)
                    {
                        GameObject obj = Instantiate(Items[itemNum].item, player.transform);
                        obj.SetActive(false);
                        player.GetComponent<PlayerControls>().Inventory[i] = obj;
                        player.GetComponent<PlayerControls>().SwitchSlots();

                        UIVar.UIs[accessIndex].SetValue((int.Parse(UIVar.UIs[accessIndex].GetValue()) - Items[itemNum].itemCost).ToString());
                        audio.Play();
                        break;
                    }
                }
            }
        }
    }
}
