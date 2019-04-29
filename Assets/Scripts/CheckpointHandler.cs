using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour,IInteract
{
    public Sprite active;
    public Sprite inactive;
    public bool isActive;

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
            MakeActive();
        }
    }

    public void MakeActive()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().startLocation = this.gameObject.transform.position + transform.up;
        sr.sprite = active;
        isActive = true;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            if (obj.GetComponent<CheckpointHandler>().isActive && obj != this.gameObject)
            {
                obj.GetComponent<CheckpointHandler>().MakeInactive();
            }
        }
    }

    public void MakeInactive()
    {
        sr.sprite = inactive;
        isActive = false;
    }
}
