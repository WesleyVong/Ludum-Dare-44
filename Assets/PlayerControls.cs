using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerControls : MonoBehaviour
{
    public KeyCode right;
    public KeyCode left;
    public KeyCode up;
    public KeyCode trigger;
    public bool contact = false;
    public float jumpCooldown = 0.5f;
    public GameObject onHand;

    private UIVariables UIVar;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool canJump = true;
    // By default is facing right
    private bool facingRight = true;
    private Vector2 startLocation;
    private float jumpTimer;

    private void Start()
    {
        UIVar = GameObject.Find("Scene").GetComponent<UIVariables>();
        startLocation = transform.position;
        rb = GetComponent<Rigidbody2D>();

        // Default without flip is right
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(right))
        {
            if (contact)
            {
                rb.AddForce(Vector2.right * 40);
            }
            else
            {
                rb.AddForce(Vector2.right * 20);
            }
            if (!facingRight)
            {
                flip();
            }
        }
        if (Input.GetKey(left))
        {
            if (contact)
            {
                rb.AddForce(Vector2.left * 40);
            }
            else
            {
                rb.AddForce(Vector2.left * 20);
            }
            if (facingRight)
            {
                flip();
            }
        }
        if (Input.GetKey(up) && contact && canJump)
        {
            rb.AddForce(Vector2.up * 1000);
            jumpTimer = jumpCooldown;
            canJump = false;
        }
        if (Input.GetKey(trigger))
        {
            var objectsOnHand = onHand.GetComponents<MonoBehaviour>();
            IHandHeld[] interfaceScripts = (from a in objectsOnHand where a.GetType().GetInterfaces().Any(k => k == typeof(IHandHeld)) select (IHandHeld)a).ToArray();
            foreach (var iScript in interfaceScripts)
            {
                iScript.Trigger();
            }

        }

        jumpTimer -= Time.deltaTime;

        // Death Conditions
        if (transform.position.y < -20)
        {
            Death();
        }
    }

    private void flip()
    {
        sr.flipX = !sr.flipX;
        onHand.transform.localPosition = new Vector2(onHand.transform.localPosition.x * -1, onHand.transform.localPosition.y);
        onHand.GetComponent<SpriteRenderer>().flipX = !onHand.GetComponent<SpriteRenderer>().flipX;
        facingRight = !facingRight;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && jumpTimer <= 0)
        {
            canJump = true;
        }
        contact = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contact = false;
    }

    public void Death()
    {
        if (int.Parse(UIVar.UIs[0].GetValue()) > 0)
        {
            rb.velocity = new Vector2();
            transform.position = startLocation;
            UIVar.UIs[0].SetValue((int.Parse(UIVar.UIs[0].GetValue()) - 1).ToString());
        }
        
    }
}
