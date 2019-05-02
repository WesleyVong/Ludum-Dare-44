using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class SimpleAI : MonoBehaviour, IPlayer
{
    private Tilemap tilemap;
    public UIBarUpdate UIBar;

    // Bot Settings
    public GameObject[] drops;
    [Tooltip("Needed for drops like weapons and keys")]
    public GameObject dropPrefab;
    public GameObject target;
    public GameObject onHand;
    public float moveSpeed = 20;
    public float jumpStrength = 1000;
    [Tooltip("Delay between detecting player and firing")]
    public float fireDelay = 0.1f;
    public float jumpCooldown;
    public float health = 10;
    [Tooltip("Range of Detection of Player")]
    public float attackRange = 10;
    [Tooltip("Make the number larger for larger colliders and smaller for smaller colliders")]
    public float detectionDistance = 0.5f;
    public bool wander;
    public bool followTarget;
    public bool autoJump;
    public bool avoidCliffs;
    public bool attackPlayer;
    [Tooltip("Attacks players when they get next to the enemy")]
    public bool melee;
    public bool trackingWeapon;

    private bool isPaused;

    // Private Values
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool goRight;
    private bool canJump;
    private bool contact;

    // Timers
    private float jumpTimer;
    private float fireDelayTimer;
    private float wanderTimer;

    // Offset from transform.position
    private float lowestPoint;
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
        if (!isPaused)
        {
            position = transform.position;
            targetLocation = target.transform.position;
            distanceVector = targetLocation - position;

            if (wander)
            {
                if (wanderTimer > 0)
                {
                    if (avoidCliffs)
                    {

                        if (NextToCliff(goRight) == false)
                        {
                            Move();
                        }
                    }
                    else
                    {
                        Move();
                    }
                }
                else
                {
                    wanderTimer = Random.Range(0.5f, 2f);
                    goRight = !goRight;
                }

            }

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
                        Move();
                    }
                }
                else
                {
                    Move();
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
                Death();
            }

            fireDelayTimer -= Time.deltaTime;
            jumpTimer -= Time.deltaTime;
            wanderTimer -= Time.deltaTime;
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

    private void Move()
    {
        if (goRight)
        {
            if (contact)
            {
                rb.AddForce(Vector2.right * moveSpeed);
            }
            else
            {
                rb.AddForce(Vector2.right * moveSpeed/2);
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
                rb.AddForce(Vector2.left * moveSpeed);
            }
            else
            {
                rb.AddForce(Vector2.left * moveSpeed/2);
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
                tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x + detectionDistance, GetComponent<Collider2D>().bounds.min.y - (detectionDistance), 0))) == null &&
                tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x + detectionDistance, GetComponent<Collider2D>().bounds.min.y - (detectionDistance + 1), 0))) == null)
        {
            return true;
        }
        if (!right &&
            // Block to the bottom left exists
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x - detectionDistance, GetComponent<Collider2D>().bounds.min.y - (detectionDistance), 0))) == null &&
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x - detectionDistance, GetComponent<Collider2D>().bounds.min.y - (detectionDistance + 1), 0))) == null)
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
                tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x + detectionDistance, GetComponent<Collider2D>().bounds.min.y, 0))) != null &&
                // Block to the right top does not exist
                tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x + detectionDistance, GetComponent<Collider2D>().bounds.min.y + detectionDistance + 1, 0))) == null)
        {
            if (contact && canJump)
            {
                Jump(jumpStrength);
            }
        }
        if (!facingRight &&
            // Block to the left exists
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x - detectionDistance, GetComponent<Collider2D>().bounds.min.y, 0))) != null &&
            // Block to the left top does not exist
            tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(GetComponent<Collider2D>().bounds.min.x - detectionDistance, GetComponent<Collider2D>().bounds.min.y + detectionDistance + 1, 0))) == null)
        {
            if (contact && canJump)
            {
                Jump(jumpStrength);
            }
        }
    }

    private void AttackPlayer()
    {
        RaycastHit2D hit;
        if (facingRight)
        {
            hit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.center, new Vector2(Mathf.Abs(target.transform.position.x - transform.position.x), Mathf.Clamp(target.transform.position.y - transform.position.y, -1f, 1f)).normalized, attackRange);
        } else
        {
            hit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.center, new Vector2(-Mathf.Abs(target.transform.position.x - transform.position.x), Mathf.Clamp(target.transform.position.y - transform.position.y, -1f, 1f)).normalized, attackRange);
        }
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == target)
            {
                if (fireDelayTimer <= 0)
                {
                    var objectsOnHand = onHand.GetComponents<MonoBehaviour>();
                    IHandHeld[] interfaceScripts = (from a in objectsOnHand where a.GetType().GetInterfaces().Any(k => k == typeof(IHandHeld)) select (IHandHeld)a).ToArray();
                    foreach (var iScript in interfaceScripts)
                    {
                        if (trackingWeapon)
                        {
                            if (onHand.GetComponent<TrackPlayer>().aimed)
                            {
                                iScript.Trigger();
                            }
                        }
                        else
                        {
                            iScript.Trigger();
                        }
                    }
                }
            }
            else
            {
                fireDelayTimer = fireDelay;
            }
        }
        else
        {
            fireDelayTimer = fireDelay;
        }
    }

    private void flip()
    {
        sr.flipX = !sr.flipX;
        onHand.transform.localPosition = new Vector2(onHand.transform.localPosition.x * -1, onHand.transform.localPosition.y);
        if (!trackingWeapon)
        {
            onHand.GetComponent<SpriteRenderer>().flipX = !onHand.GetComponent<SpriteRenderer>().flipX;
        }
        facingRight = !facingRight;
    }

    public void Death()
    {
        foreach (GameObject drop in drops)
        {
            if (drop.GetComponent<Rigidbody2D>() != null)
            {
                GameObject obj = Instantiate(drop, transform.position, transform.rotation);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), 2));
            }
            else
            {
                GameObject obj = Instantiate(dropPrefab, transform.position, transform.rotation);
                obj.GetComponent<ItemPickup>().item = drop;
                obj.GetComponent<ItemPickup>().gracePeriod(0.5f);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), 2));
            }
        }
        
        Destroy(gameObject);
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }
}
