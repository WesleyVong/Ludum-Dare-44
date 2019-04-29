﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour, IInteract
{
    public bool needsKey;
    [Tooltip("Removes key when door opens")]
    public bool removeKey;
    public bool isOpen = false;
    public Sprite openSprite;
    public Sprite closeSprite;
    public AudioSource opening;
    public AudioSource locked;
    public GameObject otherDoor;

    private SpriteRenderer sr;

    [Tooltip("Needed if the door needs a key")]
    public string keyTag;

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
                if (needsKey &&
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().Inventory[GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().selected].tag == keyTag)
                {
                    if (removeKey)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().Inventory[GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().selected] = null;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().SwitchSlots();
                    }
                    ToggleDoor();
                }
                else if (!needsKey)
                {
                    ToggleDoor();
                }
                else
                {
                    locked.Play();
                }
            }
            else
            {
                if (otherDoor.GetComponent<DoorHandler>().needsKey && !otherDoor.GetComponent<DoorHandler>().isOpen)
                {
                    otherDoor.GetComponent<DoorHandler>().ToggleDoor();
                }
                GameObject.FindGameObjectWithTag("Player").transform.position = otherDoor.transform.position + transform.up;
            }
            
        }
    }

    public void ToggleDoor()
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
