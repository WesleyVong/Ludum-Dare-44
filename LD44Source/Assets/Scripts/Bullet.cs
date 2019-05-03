using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour, IProjectile
{
    public float velocity = 50;
    public float damage = 1;
    [Tooltip("Timer until self-destruction")]
    public float lifeTime = 1f;
    public bool facingRight = true;
    [Tooltip("Enemies that can be hit before the bullet gets destroyed")]
    public int hittableEnemies = 1;

    private GameObject Shooter;
    private Rigidbody2D rb;
    private Vector2 initialVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialVelocity = new Vector2(Mathf.Abs(transform.parent.parent.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(transform.parent.parent.GetComponent<Rigidbody2D>().velocity.x));
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
                if (hittableEnemies - 1 <= 0 || col.tag == "Ground") 
                {
                    Destroy(gameObject);
                }
                hittableEnemies -= 1;
                damage /= 2;
            }
        }
    }

    public void Direction(bool dir, float dmg)
    {
        facingRight = dir;
        damage *= dmg;
    }
}
