using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIVariables : MonoBehaviour
{
    public UIHandler[] UIs;

    private int sceneNum;

    private void Start()
    {
        foreach (UIHandler ui in UIs)
        {
            ui.Load();
        }
        foreach (UIHandler ui in UIs)
        {
            ui.UpdateUI();
        }
    }

    public void SaveAll()
    {
        foreach (UIHandler ui in UIs)
        {
            ui.Save();
        }
    }


    public void ResetUI()
    {
        foreach (UIHandler ui in UIs)
        {
            ui.Reset();
        }
    }
}
