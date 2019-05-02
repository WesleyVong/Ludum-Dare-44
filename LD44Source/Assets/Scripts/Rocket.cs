using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rocket : MonoBehaviour, IProjectile
{
    public float velocity = 25;
    public float damage = 1;
    public float radius = 1;
    [Tooltip("Delay before detecting collisions")]
    public float delay = 0;
    public bool facingRight = true;

    private ParticleSystem ps;
    private GameObject Shooter;
    private Rigidbody2D rb;
    private Vector2 initialVelocity;
    private bool isDead;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        initialVelocity = new Vector2(Mathf.Abs(transform.parent.parent.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(transform.parent.parent.GetComponent<Rigidbody2D>().velocity.x)); ;
        Shooter = transform.parent.parent.gameObject;
        transform.parent = transform.parent.parent;
    }

    private void Update()
    {
        if (facingRight && !isDead)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            rb.velocity = transform.right * (initialVelocity.x + velocity);
        }
        if (!facingRight && !isDead)
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
            if (col.gameObject != Shooter)
            {
                foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,radius))
                {
                    if (obj.gameObject != Shooter)
                    {
                        var objectScripts = obj.GetComponents<MonoBehaviour>();
                        IPlayer[] interfaceScripts = (from a in objectScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IPlayer)) select (IPlayer)a).ToArray();
                        foreach (var iScript in interfaceScripts)
                        {
                            iScript.Damage(damage - ((obj.transform.position - transform.position).magnitude / radius));
                        }
                    }
                }
                isDead = true;
                ps.Play();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, (ps.duration));

            }
        }
    }

    public void Direction(bool dir, float dmg)
    {
        facingRight = dir;
        damage *= dmg;
    }
}
