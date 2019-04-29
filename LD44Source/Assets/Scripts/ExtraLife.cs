using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public UIVariables UIVar;
    public AudioSource audio;
    public int accessedValueIndex = 0;
    public int change = 1;

    private void Start()
    {
        if (UIVar == null)
        {
            UIVar = GameObject.Find("Scene").GetComponent<UIVariables>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            try
            {
                UIVar.UIs[accessedValueIndex].SetValue((int.Parse(UIVar.UIs[accessedValueIndex].GetValue()) + change).ToString());
            }
            catch
            {
                UIVar.UIs[accessedValueIndex].SetValue((float.Parse(UIVar.UIs[accessedValueIndex].GetValue()) + change).ToString());
            }
            audio.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, audio.clip.length);
        }
    }
}
