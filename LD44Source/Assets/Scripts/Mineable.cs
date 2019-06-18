using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineable : MonoBehaviour, IInteract
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().inTrigger.Add(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().inTrigger.Remove(this.gameObject);
        }
    }

    public void OnInteract()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().Inventory[GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().selected].tag == "Pickaxe")
        {
            Destroy(gameObject);
        }
    }

}
