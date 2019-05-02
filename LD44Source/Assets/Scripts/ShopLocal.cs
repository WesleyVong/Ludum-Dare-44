using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLocal : MonoBehaviour, IInteract
{
    public ShopHandler shopUI;
    public ShopItem[] Items;
    public GameObject ShopPanel;



    private bool withinTrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inTrigger.Add(this.gameObject);
            withinTrigger = true;
            shopUI.SetItems(Items);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inTrigger.Remove(this.gameObject);
            withinTrigger = false;
            if (ShopPanel.activeInHierarchy)
            {
                ShopPanel.SetActive(false);
            }
        }

    }

    public void OnInteract()
    {
        if (withinTrigger && !ShopPanel.activeInHierarchy)
        {
            ShopPanel.SetActive(true);
        }
        else if (ShopPanel.activeInHierarchy)
        {
            ShopPanel.SetActive(false);
        }
    }
}
