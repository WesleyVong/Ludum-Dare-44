using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{
    private Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetText(string txt)
    {
        Debug.Log("Text Set");
        text.text = txt;
        StartCoroutine(Fade(2));
    }
    public IEnumerator Fade(float time)
    {
        yield return new WaitForSeconds(time);
        text.text = "";
    }
}
