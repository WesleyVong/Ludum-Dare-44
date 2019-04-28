using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour, IPlayer
{
    // Controls
    public KeyCode right;
    public KeyCode left;
    public KeyCode up;
    public KeyCode trigger;
    public KeyCode interact;
    public KeyCode reload;

    public GameObject ShopPanel;
    public GameObject GameOverPanel;
    public bool withinShop;

    public Tilemap tilemap;

    public bool contact = false;
    public bool autoJump = true;
    public float jumpCooldown = 0.5f;

    public AudioSource audio;

    // Inventory System
    public GameObject onHand;
    public int selected = 2;
    public GameObject[] Inventory = new GameObject[5];

    private float health;
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
        SwitchSlots();
    }

    void Update()
    {
        if ((Input.GetAxis("Mouse ScrollWheel")) != 0 && 
            (selected + (int)(Input.GetAxis("Mouse ScrollWheel") * 10)) <= 4 && 
            (selected + (int)(Input.GetAxis("Mouse ScrollWheel") * 10) >= 0))
        {
            selected += (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
            SwitchSlots();
        }
        if (Input.GetKey("1"))
        {
            selected = 0;
            SwitchSlots();
        }

        if (Input.GetKey("2"))
        {
            selected = 1;
            SwitchSlots();
        }

        if (Input.GetKey("3"))
        {
            selected = 2;
            SwitchSlots();
        }

        if (Input.GetKey("4"))
        {
            selected = 3;
            SwitchSlots();
        }

        if (Input.GetKey("5"))
        {
            selected = 4;
            SwitchSlots();
        }


        if (Input.GetKey(right))
        {
            if (autoJump)
            {
                AutoJump();
            }
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
            if (autoJump)
            {
                AutoJump();
            }
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

        // Jump
        if (Input.GetKey(up) && contact && canJump)
        {
            Jump(1000);
        }


        if (Input.GetKey(trigger))
        {
            if (onHand != null)
            {
                var objectsOnHand = onHand.GetComponents<MonoBehaviour>();
                IHandHeld[] interfaceScripts = (from a in objectsOnHand where a.GetType().GetInterfaces().Any(k => k == typeof(IHandHeld)) select (IHandHeld)a).ToArray();
                foreach (var iScript in interfaceScripts)
                {
                    iScript.Trigger();
                }
            }
        }
        if (Input.GetKeyDown(reload))
        {
            if (onHand != null)
            {
                try
                {
                    onHand.GetComponent<Gun>().Reload();
                }
                catch
                {

                }
                    
            }
        }
        if (Input.GetKeyDown(interact))
        {
            if (withinShop && !ShopPanel.activeInHierarchy)
            {
                ShopPanel.SetActive(true);
            }
            else if (ShopPanel.activeInHierarchy)
            {
                ShopPanel.SetActive(false);
            }
        }

        jumpTimer -= Time.deltaTime;

        // Death Conditions
        if (transform.position.y < -20f ||
            float.Parse(UIVar.UIs[2].GetValue()) <= 0)
        {
            Death();
        }
    }

    private void SwitchSlots()
    {
        if (onHand != null)
        {
            onHand.SetActive(false);
        }
        onHand = Inventory[selected];
        if (onHand != null)
        {
            onHand.SetActive(true);
            onHand.GetComponent<SpriteRenderer>().flipX = !facingRight;
            if (!facingRight && onHand.transform.localPosition.x > 0)
            {
                onHand.transform.localPosition *= -1;
            }
            if (facingRight && onHand.transform.localPosition.x < 0)
            {
                onHand.transform.localPosition *= -1;
            }
        }
    }

    private void flip()
    {
        sr.flipX = !sr.flipX;
        if (onHand != null)
        {
            onHand.transform.localPosition = new Vector2(onHand.transform.localPosition.x * -1, onHand.transform.localPosition.y);
            onHand.GetComponent<SpriteRenderer>().flipX = !onHand.GetComponent<SpriteRenderer>().flipX;
        }
        facingRight = !facingRight;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (jumpTimer <= 0)
        {
            canJump = true;
        }
        contact = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contact = false;
    }

    private void Jump(float strength)
    {
        rb.AddForce(Vector2.up * strength);
        jumpTimer = jumpCooldown;
        canJump = false;
    }

    private void AutoJump()
    {
        if (facingRight &&
            // Block to the right exists
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z))) != null &&
            // Block to the right top does not exist
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x + 0.5f, transform.position.y + 1, transform.position.z))) == null)
        {
            if (contact && canJump)
            {
                Jump(750);
            }
        }
        if (!facingRight &&
            // Block to the left exists
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z))) != null &&             // Block to the left top does not exist
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x - 0.5f, transform.position.y + 1, transform.position.z))) == null)
        {
            if (contact && canJump)
            {
                Jump(750);
            }
        }
    }

    public void Damage(float dmg)
    {
        UIVar.UIs[2].SetValue((float.Parse(UIVar.UIs[2].GetValue()) - dmg).ToString());
    }

    public void Death()
    {
        if (int.Parse(UIVar.UIs[0].GetValue()) > 0)
        {
            rb.velocity = new Vector2();
            transform.position = startLocation;
            UIVar.UIs[0].SetValue((int.Parse(UIVar.UIs[0].GetValue()) - 1).ToString());
            UIVar.ResetUI();
            audio.Play();
        }
        else
        {
            GameOverPanel.SetActive(true);
            Destroy(gameObject);
        }
        
    }
}
