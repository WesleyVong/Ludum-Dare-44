using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionText : MonoBehaviour
{
    public float yOffset;
    public float xOffset;

    private ShopHandler sh;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        sh = GameObject.Find("Shop").GetComponent<ShopHandler>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(xOffset,yOffset,0);
    }

    public void UpdateText(int index)
    {
        text.text = sh.GetItem(index).itemDesc;
    }

    public void RemoveText()
    {
        text.text = "";
    }


}
