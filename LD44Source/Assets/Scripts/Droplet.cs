using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    public ParticleSystem ps;
    public AudioSource audio;

    public float timer = 5;

    private bool isDying;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (isDying && !audio.isPlaying && !ps.isPlaying)
        {
            Destroy(gameObject);
        }
        else if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audio.Play();
        ps.Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        isDying = true;
    }
}
