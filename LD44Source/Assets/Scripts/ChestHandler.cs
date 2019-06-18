using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : MonoBehaviour, IInteract
{
    public bool isOpen = false;
    public Sprite openSprite;
    public Sprite closeSprite;
    public GameObject item;
    public GameObject dropPrefab;
    public AudioSource opening;

    private SpriteRenderer sr;

    private bool withinTrigger;

    private string ID;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ID = transform.position.sqrMagnitude.ToString();
        if (PlayerPrefs.GetString(ID,"False") == "True")
        {
            ToggleChest();
        }
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
                GameObject obj = Instantiate(dropPrefab, transform.position, transform.rotation);
                obj.GetComponent<ItemPickup>().item = item;
                if (obj.GetComponent<Rigidbody2D>() != null)
                {
                    obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), 2));
                }
                PlayerPrefs.SetString(ID, isOpen.ToString());
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
