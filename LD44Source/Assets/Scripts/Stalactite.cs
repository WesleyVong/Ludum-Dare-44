using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public GameObject droplet;
    public float dripTime = 10;

    private float timer;

    private void Start()
    {
        timer = Random.Range(0.1f, dripTime);
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Instantiate(droplet, transform);
            timer = Random.Range(0.1f, dripTime);
        }
        timer -= Time.deltaTime;
    }
}
