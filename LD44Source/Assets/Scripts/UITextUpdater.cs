using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextUpdater : MonoBehaviour, IUI
{

    private string max;
    public void UpdateUI(string val)
    {
        try
        {
            GetComponent<Text>().text = (int.Parse(val)).ToString();
        }
        catch
        {
            GetComponent<Text>().text = (float.Parse(val)).ToString("F2");
        }


    }

    public void UpdateMax(string val)
    {
        max = val;
    }
}
