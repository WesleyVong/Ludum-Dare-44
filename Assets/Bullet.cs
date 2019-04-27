using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 50;
    [Tooltip("Delay before detecting collisions")]
    public float delay = 0.02f;
    public bool facingRight = true;

    private Rigidbody2D rb;
    private Vector2 initialVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialVelocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity;
    }

    private void Update()
    {
        if (facingRight)
        {
            rb.velocity = initialVelocity + Vector2.right * velocity;
        }
        if (!facingRight)
        {
            rb.velocity = initialVelocity + Vector2.left * velocity;
        }
        delay -= Time.deltaTime;
    }

    private void OnTriggerStay2D()
    {
        if (delay <= 0)
        {
            Destroy(gameObject);
        }
    }
}
