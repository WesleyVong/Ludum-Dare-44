using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public UIVariables UIVar;
    public AudioSource audio;

    private void Start()
    {
        if (UIVar == null)
        {
            UIVar = GameObject.Find("Scene").GetComponent<UIVariables>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIVar.UIs[0].SetValue((int.Parse(UIVar.UIs[0].GetValue()) + 1).ToString());
            audio.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, audio.clip.length);
        }
    }
}
