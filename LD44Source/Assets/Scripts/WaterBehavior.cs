using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.attachedRigidbody.drag *= 10;
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inWater = true;
            collision.GetComponent<PlayerControls>().moveSpeed = 60;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.attachedRigidbody.drag /= 10;
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inWater = false;
            collision.GetComponent<PlayerControls>().moveSpeed = 40;
        }
    }
}
