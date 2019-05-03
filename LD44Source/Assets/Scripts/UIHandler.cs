using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class UIHandler
{
    public GameObject[] UIs;
    public string value;
    public string startValue;
    public bool autoUpdate = true;
    public bool doNotReset;

    public UIHandler()
    {
        UIs = null;
        value = "";
    }
    public UIHandler(GameObject[] obj, string val)
    {
        UIs = obj;
        value = val;
    }
    public void SetValue(string val)
    {
        try
        {
            if (float.Parse(val) <= float.Parse(startValue))
            {
                value = val;
            }
            else
            {
                value = startValue;
            }
        }
        catch
        {
            value = val;
        }

        if (startValue == "")
        {
            SetResetValue(value);
        }

        if (autoUpdate)
        {
            UpdateUI();
        }
    }

    public string GetValue()
    {
        return value;
    }

    public void SetResetValue(string val)
    {
        startValue = val;
    }

    public void UpdateUI()
    {
        foreach (GameObject UI in UIs)
        {
            var UIScripts = UI.GetComponents<MonoBehaviour>();
            IUI[] interfaceScripts = (from a in UIScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IUI)) select (IUI)a).ToArray();
            foreach (var iScript in interfaceScripts)
            {
                iScript.UpdateUI(value);
                iScript.UpdateMax(startValue);
            }
        }
    }

    public void Reset()
    {
        if (!doNotReset)
        {
            value = startValue;
            UpdateUI();
        }
    }
}
