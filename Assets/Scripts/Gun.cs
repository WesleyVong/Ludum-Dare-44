using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IHandHeld
{
    [Tooltip("Rounds per Second")]
    public float fireRate = 2;

    [Tooltip("Seconds")]
    public float reloadTime = 5;

    public int rounds = 6;
    public GameObject bulletObj;

    public int remaining;

    private string tag;
    private bool reloading;
    private float reloadTimer;
    private float cooldownTimer;

    public AudioSource shoot;
    public AudioSource noAmmo;

    private UIVariables UIVar;

    void Start()
    {
        UIVar = GameObject.Find("Scene").GetComponent<UIVariables>();
        remaining = rounds;
        tag = transform.parent.tag;
    }

    public void Trigger()
    {
        if (cooldownTimer <= 0 && !reloading)
        {
            cooldownTimer = (1.0f / fireRate);
            remaining-= 1;
            GameObject bullet = Instantiate(bulletObj, transform.position,transform.rotation,transform);
            bullet.GetComponent<Bullet>().facingRight = !GetComponent<SpriteRenderer>().flipX;
            shoot.Play();
        }
        if (cooldownTimer <= 0 && reloading)
        {
            cooldownTimer = (1.0f / fireRate);
            noAmmo.Play();
        }

    }

    public void Reload()
    {
        reloadTimer = reloadTime;
        reloading = true;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;
        if (remaining <= 0 && reloadTimer < 0 && !reloading)
        {
            Reload();
        }
        if (reloadTimer <= 0 && reloading)
        {
            remaining = rounds;
            reloading = false;
        }
        if (tag == "Player")
        {
            UIVar.UIs[1].SetValue(remaining.ToString());
        }
    }
}
