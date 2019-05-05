using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarUpdate : MonoBehaviour, IUI
{

    public bool isUI = true;

    private float max;
    private float overload;

    // Default min is 0
    private float min = 0;
    private float value;

    private float xPos;
    private float yPos;
    private float xScale;

    void Awake()
    {
        if (isUI)
        {
            xPos = GetComponent<RectTransform>().anchoredPosition.x;
            yPos = GetComponent<RectTransform>().anchoredPosition.y;
            xScale = GetComponent<RectTransform>().localScale.x;
        }
        else
        {
            xPos = transform.localPosition.x;
            yPos = transform.localPosition.y;
            xScale = transform.localScale.x;
        }
        
    }

    public void UpdateUI(string val)
    {
        value = float.Parse(val);
        // Maximum will be any value that exceeds it
        if (max == 0)
        {
            max = value;
        }
        if (value - max > 0)
        {
            overload = value - max;
        }
        else
        {
            overload = 0;
        }
        xScale = value / ((max + overload) - min);

        if (isUI)
        {
            GetComponent<RectTransform>().localScale = new Vector3(xScale, 1, 1);
            GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos * xScale, yPos, 0);
        }
        else
        {
            transform.localScale = new Vector3(xScale, 1, 1);
        }
        

    }

    public void UpdateMax(string val)
    {
        string tmp = val;
        try
        {
            max = int.Parse(tmp);
        }
        catch
        {
            max = float.Parse(tmp);
        }
    }

    public float GetMax()
    {
        return (max);
    }
}
