using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarUpdate : MonoBehaviour, IUI
{

    public bool isUI = true;

    private float max;

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
        if (value > max)
        {
            max = value;
        }
        xScale = value / (max - min);
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

    public void setMinMax(float mn, float mx)
    {
        min = mn;
        max = mx;
    }

    public (float,float) getMinMax()
    {
        return (min,max);
    }
}
