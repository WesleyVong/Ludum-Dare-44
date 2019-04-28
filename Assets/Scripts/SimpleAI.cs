using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class SimpleAI : MonoBehaviour, IPlayer
{
    public Tilemap tilemap;
    public UIBarUpdate UIBar;

    // Bot Settings
    public GameObject drop;
    public GameObject target;
    public GameObject onHand;
    public float jumpCooldown;
    public float health = 10;
    public bool followTarget;
    public bool autoJump;
    public bool avoidCliffs;
    public bool attackPlayer;
    [Tooltip("Attacks players when they get next to the enemy")]
    public bool melee;

    // Private Values
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool goRight;
    private bool canJump;
    private bool contact;
    private float jumpTimer;
    private SpriteRenderer sr;

    // Target Tracking
    private Vector2 targetLocation;
    private Vector2 distanceVector;
    private Vector2 position;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (tilemap == null)
        {
            tilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        }
    }

    private void Update()
    {
        position = transform.position;
        targetLocation = target.transform.position;
        distanceVector = targetLocation - position;

        if (followTarget)
        {
            if (distanceVector.x > 0)
            {
                goRight = true;
            }
            else
            {
                goRight = false;
            }
            if (avoidCliffs)
            {
                
                if (NextToCliff(goRight) == false)
                {
                    FollowTarget();
                }
                else
                {
                }
            }
            else
            {
                FollowTarget();
            }
        }

        if (attackPlayer)
        {
            AttackPlayer();
        }

        if (autoJump)
        {
            AutoJump();
        }

        UIBar.UpdateUI(health.ToString());

        if (health <= 0)
        {
            Instantiate(drop, transform.position,transform.rotation);
            Destroy(gameObject);
        }
        
    }

    public void Damage(float dmg)
    {
        health -= dmg;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && jumpTimer <= 0)
        {
            canJump = true;
        }
        if (collision.gameObject.tag == "Player" && melee)
        {
            collision.gameObject.GetComponent<PlayerControls>().Damage(1 * Time.deltaTime);
        }
        contact = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contact = false;
    }

    private void FollowTarget()
    {
        if (goRight)
        {
            if (contact)
            {
                rb.AddForce(Vector2.right * 20);
            }
            else
            {
                rb.AddForce(Vector2.right * 10);
            }
            if (!facingRight)
            {
                flip();
            }
        }
        else
        {
            if (contact)
            {
                rb.AddForce(Vector2.left * 20);
            }
            else
            {
                rb.AddForce(Vector2.left * 10);
            }
            if (facingRight)
            {
                flip();
            }
        }
    }

    private bool NextToCliff(bool right)
    {
        if (right &&
                // Block to the bottom right exists
                tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z))) == null)
        {
            return true;
        }
        if (!right &&
            // Block to the bottom left exists
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z))) == null)
        {
            return true;
        }
        return false;
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
                Jump(1000);
            }
        }
        if (!facingRight &&
            // Block to the left exists
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z))) != null &&
            // Block to the left top does not exist
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(transform.position.x - 0.5f, transform.position.y + 1, transform.position.z))) == null)
        {
            if (contact && canJump)
            {
                Jump(1000);
            }
        }
    }

    private void AttackPlayer()
    {
        RaycastHit2D hit;
        if (facingRight)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right);
        } else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left);
        }
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == target)
            {
                var objectsOnHand = onHand.GetComponents<MonoBehaviour>();
                IHandHeld[] interfaceScripts = (from a in objectsOnHand where a.GetType().GetInterfaces().Any(k => k == typeof(IHandHeld)) select (IHandHeld)a).ToArray();
                foreach (var iScript in interfaceScripts)
                {
                    iScript.Trigger();
                }
            }
        }
    }

    private void flip()
    {
        sr.flipX = !sr.flipX;
        onHand.transform.localPosition = new Vector2(onHand.transform.localPosition.x * -1, onHand.transform.localPosition.y);
        onHand.GetComponent<SpriteRenderer>().flipX = !onHand.GetComponent<SpriteRenderer>().flipX;
        facingRight = !facingRight;
    }
}
