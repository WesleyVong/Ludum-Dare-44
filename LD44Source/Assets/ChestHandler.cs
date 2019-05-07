using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : MonoBehaviour, IInteract
{
    public bool isOpen = false;
    public Sprite openSprite;
    public Sprite closeSprite;
    public GameObject item;
    public AudioSource opening;

    private SpriteRenderer sr;

    private bool withinTrigger;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inTrigger.Add(this.gameObject);
            withinTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inTrigger.Remove(this.gameObject);
            withinTrigger = false;
        }
    }

    public void OnInteract()
    {
        if (withinTrigger)
        {
            if (!isOpen)
            {
                ToggleChest();
            }

        }
    }

    public void ToggleChest()
    {
        opening.Play();
        isOpen = !isOpen;
        if (isOpen)
        {
            sr.sprite = openSprite;
        }
        else
        {
            sr.sprite = closeSprite;
        }
    }
}
