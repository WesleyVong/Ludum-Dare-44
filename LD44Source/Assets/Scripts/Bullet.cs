using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour
{
    public float velocity = 50;
    public float damage = 1;
    [Tooltip("Delay before detecting collisions")]
    public float delay = 0;
    public bool facingRight = true;

    private GameObject Shooter;
    private Rigidbody2D rb;
    private Vector2 initialVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialVelocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity;
        Shooter = transform.parent.parent.gameObject;
        transform.parent = transform.parent.parent;
    }

    private void Update()
    {
        if (facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            rb.velocity = transform.right * (initialVelocity.x + velocity);
        }
        if (!facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            rb.velocity = -transform.right * (initialVelocity.x + velocity);
        }
        delay -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (delay <= 0)
        {
            if (col.tag == "Player" || col.tag == "Enemy" || col.tag == "Ground")
            {
                if (col.gameObject != Shooter)
                {
                    var objectScripts = col.GetComponents<MonoBehaviour>();
                    IPlayer[] interfaceScripts = (from a in objectScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IPlayer)) select (IPlayer)a).ToArray();
                    foreach (var iScript in interfaceScripts)
                    {
                        iScript.Damage(damage);
                    }
                    Destroy(gameObject);
                }
            }

        }
    }
}
