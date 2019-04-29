using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject item;

    private bool allowPickup = true;
    private bool pickedUp = false;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GetComponent<BoxCollider2D>() == null && item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            gameObject.AddComponent<BoxCollider2D>();
        }
        if (GetComponent<BoxCollider2D>() != null && !allowPickup)
        {
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
        if (GetComponent<BoxCollider2D>() != null && allowPickup)
        {
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < collision.gameObject.GetComponent<PlayerControls>().Inventory.Length; i++)
                {
                    if (collision.gameObject.GetComponent<PlayerControls>().Inventory[i] == null)
                    {
                        GameObject obj = Instantiate(item, collision.gameObject.transform);
                        obj.SetActive(false);
                        collision.gameObject.GetComponent<PlayerControls>().Inventory[i] = obj;
                        collision.gameObject.GetComponent<PlayerControls>().SwitchSlots();
                        audio.Play();
                        pickedUp = true;
                        break;
                    }
                }
            if (pickedUp)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, audio.clip.length);
            }

        }
    }

    public void gracePeriod(float s)
    {
        StartCoroutine(timer(s));
    }

    private IEnumerator timer(float t)
    {
        allowPickup = false;
        yield return new WaitForSeconds(t);
        allowPickup = true;
    }
}
