using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIHandler
{
    public GameObject UI;
    public string name;
    public string value;
    public bool autoUpdate = true;
    
    void Start()
    {
        UpdateUI();
        if (name == "")
        {
            name = UI.name;
        }
    }

    public UIHandler()
    {
        UI = null;
        value = "";
    }
    public UIHandler(GameObject obj, string val)
    {
        UI = obj;
        value = val;
    }
    public void SetValue(string val)
    {
        value = val;
        if (autoUpdate)
        {
            UpdateUI();
        }
    }

    public string GetValue()
    {
        return value;
    }

    public string GetName()
    {
        return name;
    }

    public void UpdateUI()
    {
        try
        {
            UI.GetComponent<Text>().text = value;
        } catch
        {
            Debug.Log("UI does not have Text Component");
        }
        
    }
}
