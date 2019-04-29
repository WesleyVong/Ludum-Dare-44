using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    public float magnitude;
    public float cycleTime;
    public bool isActive = true;

    private Vector2 size;
    private bool isPumping = true;
    private float timer;

    private void Start()
    {
        size = transform.localScale;
        timer = 0;
    }

    private void Update()
    {
        if (isActive)
        {
            if (isPumping)
            {
                if (timer >= cycleTime/2)
                {
                    size = transform.localScale;
                    isPumping = false;
                    timer = 0;
                }
                else
                {
                    transform.localScale = size / (magnitude * timer + 1);
                    
                }
            }
            if (!isPumping)
            {
                if (timer >= cycleTime/2)
                {
                    size = transform.localScale;
                    isPumping = true;
                    timer = 0;
                }
                else
                {
                    transform.localScale = size * (magnitude * timer + 1);
                }
            }

            timer += Time.deltaTime;
        }
    }

}
