using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour, IHandHeld
{

    public float invincibilityTime = 10;

    private ParticleSystem ps;
    private UIVariables UIVars;
    private GameObject player;
    private AudioSource audio;
    

    private void Start()
    {
        UIVars = GameObject.Find("Scene").GetComponent<UIVariables>();
        player = GameObject.FindGameObjectWithTag("Player");
        ps = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }

    public void Trigger()
    {
        if (!player.GetComponent<PlayerControls>().invincible && int.Parse(UIVars.UIs[0].GetValue()) - 1 >= 0)
        {
            try
            {
                UIVars.UIs[0].SetValue((int.Parse(UIVars.UIs[0].GetValue()) - 1).ToString());
            }
            catch
            {
                UIVars.UIs[0].SetValue((float.Parse(UIVars.UIs[0].GetValue()) - 1).ToString());
            }
            ps.Play();
            audio.Play();
            player.GetComponent<PlayerControls>().StartInvincibility(invincibilityTime);
        }
    }
}
