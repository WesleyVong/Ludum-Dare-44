using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour, IHandHeld
{
    [Tooltip("Index of the values stored in Scene")]
    public int boostIndex;
    public float boostAmount;

    private UIVariables UIVars;

    private void Start()
    {
        UIVars = GameObject.Find("Scene").GetComponent<UIVariables>();
    }

    public void Trigger()
    {
        try
        {
            UIVars.UIs[boostIndex].SetResetValue((int.Parse(UIVars.UIs[boostIndex].startValue) + boostAmount).ToString());
            UIVars.UIs[boostIndex].SetValue((int.Parse(UIVars.UIs[boostIndex].GetValue()) + boostAmount).ToString());
        }
        catch
        {
            UIVars.UIs[boostIndex].SetResetValue((float.Parse(UIVars.UIs[boostIndex].startValue) + boostAmount).ToString());
            UIVars.UIs[boostIndex].SetValue((float.Parse(UIVars.UIs[boostIndex].GetValue()) + boostAmount).ToString());
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().Inventory[GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().selected] = null;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().SwitchSlots();
        Destroy(gameObject);
    }
}
