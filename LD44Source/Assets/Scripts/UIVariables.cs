using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVariables : MonoBehaviour
{
    public UIHandler[] UIs;

    private void Start()
    {
        foreach (UIHandler ui in UIs)
        {
            ui.UpdateUI();
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
