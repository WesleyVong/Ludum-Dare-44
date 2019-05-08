using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignHandler : MonoBehaviour, IInteract
{
    public string text;
    private TextMesh tm;
    private void Start()
    {
        tm = GetComponentInChildren<TextMesh>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inTrigger.Add(this.gameObject);
            //withinTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().inTrigger.Remove(this.gameObject);
            //withinTrigger = false;
        }
    }

    public void OnInteract()
    {
        tm.text = text;
        StartCoroutine(Fade(2));
    }

    public IEnumerator Fade(float time)
    {
        yield return new WaitForSeconds(time);
        tm.text = "";
    }
}
