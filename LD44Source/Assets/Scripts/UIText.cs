using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour, IUI
{

    private string max;

    public void UpdateUI(string val)
    {
        try
        {
            GetComponent<Text>().text = (int.Parse(val)).ToString() + " / " + max;
        }
        catch
        {
            try
            {
                GetComponent<Text>().text = (float.Parse(val)).ToString("F2") + "/" + max; ;
            }
            catch
            {
                GetComponent<Text>().text = val;
            }

        }
    }
    public void UpdateMax(string val)
    {
        max = val;
    }
}
