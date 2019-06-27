using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ladder : MonoBehaviour, IInteract
{
    private bool withinTrigger = false;

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
            if (collision.GetComponent <PlayerControls>().onLadder == true)
            {
                collision.GetComponent<PlayerControls>().onLadder = false;
                foreach (GameObject ladder in collision.GetComponent<PlayerControls>().inTrigger)
                {
                    if (ladder.tag == "Ladder")
                    {
                        collision.GetComponent<PlayerControls>().onLadder = true;
                    }
                }
            }
            
                
            
            withinTrigger = false;
        }
    }

    public void OnInteract()
    {
        if (withinTrigger)
        {
            Debug.Log("Triggered");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().onLadder = !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().onLadder;

        }
    }
}
