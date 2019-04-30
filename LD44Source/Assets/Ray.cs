using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ray : MonoBehaviour, IProjectile
{
    public float velocity = 50;
    public float damage = 1;
    [Tooltip("Timer until self-destruction")]
    public float lifeTime = 0.2f;
    public float maxSizeMultipler = 2f;
    public bool facingRight = true;

    private float startTime;
    private GameObject Shooter;
    private Rigidbody2D rb;
    private Vector2 initialVelocity;
    private Vector2 initialSize;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialVelocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity;
        Shooter = transform.parent.parent.gameObject;
        transform.parent = transform.parent.parent;
        initialSize = transform.localScale;
        startTime = lifeTime;
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

        transform.localScale = initialSize * ((maxSizeMultipler * ((startTime - lifeTime)/startTime)) + 1);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
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

    public void Direction(bool dir, float dmg)
    {
        facingRight = dir;
        damage *= dmg;
    }
}
