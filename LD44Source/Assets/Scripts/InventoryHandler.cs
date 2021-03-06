﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    private int selected;
    private GameObject[] items = new GameObject[5];
    public GameObject[] slots = new GameObject[5];
    public GameObject selector;
    public GameObject player;


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
