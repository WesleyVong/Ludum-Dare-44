using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextUpdater : MonoBehaviour, IUI
{
    public void UpdateUI(string val)
    {
        try
        {
            GetComponent<Text>().text = (int.Parse(val)).ToString();
        }
        catch
        {
            GetComponent<Text>().text = (float.Parse(val)).ToString("0.##");
        }
    }
}
