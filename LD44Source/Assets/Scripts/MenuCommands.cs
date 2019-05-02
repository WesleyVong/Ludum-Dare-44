using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCommands : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ToggleMusic()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
