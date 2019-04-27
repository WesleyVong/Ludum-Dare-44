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

    private bool reloading;
    private float reloadTimer;
    private float cooldownTimer;
    private AudioSource audio;

    private UIVariables UIVar;

    void Start()
    {
        UIVar = GameObject.Find("Scene").GetComponent<UIVariables>();
        remaining = rounds;
        audio = GetComponent<AudioSource>();
    }

    public void Trigger()
    {
        if (cooldownTimer <= 0 && !reloading)
        {
            cooldownTimer = (1.0f / fireRate);
            remaining-= 1;
            GameObject bullet = Instantiate(bulletObj, transform.position,transform.rotation,transform);
            bullet.GetComponent<Bullet>().facingRight = !GetComponent<SpriteRenderer>().flipX;
            audio.Play();
            Debug.Log("Fire");
        }
        
    }

    public void Reload()
    {
        reloadTimer = reloadTime;
        reloading = true;
        Debug.Log("Reloading");
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;
        if (remaining <= 0 && reloadTimer < 0 && !reloading)
        {
            Reload();
        }
        if (reloadTimer <= 0 && remaining <= 0 && reloading)
        {
            remaining = rounds;
            reloading = false;
        }
        UIVar.UIs[1].SetValue(remaining.ToString());
    }
}
