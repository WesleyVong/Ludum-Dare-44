using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    private int selected;
    private GameObject[] items = new GameObject[5];
    public GameObject[] slots = new GameObject[5];
    public GameObject selector;
    private GameObject player;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        selected = player.GetComponent<PlayerControls>().selected;
        items = player.GetComponent<PlayerControls>().Inventory;
        selector.GetComponent<RectTransform>().anchoredPosition = new Vector2((selected * 60) - 120, 0);
        
        // Assigning the sprites for the items in the inventory slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (items[i] != null)
            {
                // Make image reflect size of the sprite
                Sprite sprite = items[i].GetComponent<SpriteRenderer>().sprite;
                float spriteX = sprite.bounds.size.x;
                float spriteY = sprite.bounds.size.y;
                // Sprite size is square or is wider than it is tall
                if (spriteX >= spriteY)
                {
                    // Makes sure sprite size reaches as large as possible
                    float factor = 50 / spriteX;
                    spriteX *= factor;
                    spriteY *= factor;
                }
                else
                // Sprite size is taller that it is wide
                {
                    // Makes sure sprite size reaches as large as possible
                    float factor = 50 / spriteY;
                    spriteX *= factor;
                    spriteY *= factor;
                }

                slots[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(spriteX, spriteY);
                slots[i].GetComponent<Image>().sprite = items[i].GetComponent<SpriteRenderer>().sprite;
                slots[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            else
            {
                slots[i].GetComponent<Image>().sprite = null;
                slots[i].GetComponent<Image>().color = new Color(115, 115, 115, 0);
            }
        }
    }
}
