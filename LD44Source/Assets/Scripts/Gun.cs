using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gun : MonoBehaviour, IHandHeld
{
    [Tooltip("Rounds per Second")]
    public float fireRate = 2;

    public float damageMultiplier = 1f;

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
        transform.parent = transform.parent.transform;
    }

    public void Trigger()
    {
        if (cooldownTimer <= 0 && !reloading)
        {
            cooldownTimer = (1.0f / fireRate);
            remaining-= 1;
            GameObject bullet = Instantiate(bulletObj, transform.position,transform.rotation,transform);
            var objs = bullet.GetComponents<MonoBehaviour>();
            IProjectile[] interfaceScripts = (from a in objs where a.GetType().GetInterfaces().Any(k => k == typeof(IProjectile)) select (IProjectile)a).ToArray();
            foreach (var iScript in interfaceScripts)
            {
                iScript.Direction(!GetComponent<SpriteRenderer>().flipX, damageMultiplier);
            }
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
        UIVar.UIs[1].SetValue("Reloading");
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
        if (tag == "Player" && !reloading)
        {
            if (UIVar == null)
            {
                UIVar = GameObject.Find("Scene").GetComponent<UIVariables>();
            }
            UIVar.UIs[1].SetValue(remaining.ToString());
            UIVar.UIs[1].SetResetValue(rounds.ToString());
        }
    }
}
