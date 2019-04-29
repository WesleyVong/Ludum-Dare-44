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
}
