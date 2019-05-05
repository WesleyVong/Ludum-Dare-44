using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISound : MonoBehaviour
{
    public AudioSource audio;
    public float minTime;
    public float maxTime;
    public float dist;

    private float timer;
    private void Update()
    {
        if (timer <= 0)
        {
            if ((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).magnitude < dist)
            {
                audio.Play();
            }
            timer = Random.Range(minTime, maxTime);
        }
        timer -= Time.deltaTime;

    }
}
