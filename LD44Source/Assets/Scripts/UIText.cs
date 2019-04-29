using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour, IUI
{
    public int AccessIndex;
    public UIVariables UIVariables;

    public void UpdateUI(string val)
    {
        try
        {
            GetComponent<Text>().text = (int.Parse(val)).ToString() + " / " + (int.Parse(UIVariables.UIs[AccessIndex].startValue)).ToString();
        }
        catch
        {
            GetComponent<Text>().text = (float.Parse(val)).ToString() + "/" + (float.Parse(UIVariables.UIs[AccessIndex].startValue)).ToString("0.##"); ;
        }
    }
}
