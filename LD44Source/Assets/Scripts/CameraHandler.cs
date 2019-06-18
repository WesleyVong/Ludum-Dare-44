using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraHandler : MonoBehaviour
{
    [Tooltip("Colors, index correlates to scene build index")]
    public Color[] bgColors;
    void Update()
    {
        GetComponent<Camera>().backgroundColor = bgColors[SceneManager.GetActiveScene().buildIndex];
    }
}
