using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUI : MonoBehaviour, 
                        IPointerEnterHandler,
                        IPointerExitHandler
{
    public int index;
    public GameObject Description;

    private void Start()
    {
        if (Description == null)
        {
            Description = GameObject.Find("Description");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Description.GetComponent<DescriptionText>().UpdateText(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Description.GetComponent<DescriptionText>().RemoveText();
    }
}
