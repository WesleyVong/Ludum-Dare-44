using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCommands : MonoBehaviour
{
    public void Quit()
    {
        Save();
        Application.Quit();
    }

    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleMusic()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void loadScene(string sceneName)
    {
        Debug.Log("Loading Scene");
        try
        {
            if (sceneName != "Start Scene")
            {
                DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
            }
            Save();
        }
        catch
        {
            Debug.Log("No Player Found");
        }
        SceneManager.LoadScene(sceneName);
    }

    public void Save()
    {
        // Save Player Position
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posX", GameObject.FindGameObjectWithTag("Player").transform.position.x);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posY", GameObject.FindGameObjectWithTag("Player").transform.position.y);

        // Save Player Inventory
        int iter = 0;
        string name;
        foreach (GameObject item in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().Inventory as GameObject[])
        {
            if (item != null)
            {
                name = item.name.Substring(0, item.name.Length - 7);
                PlayerPrefs.SetString("Slot" + iter, name);
            }
            iter++;
        }
        // Player Values
        GameObject.Find("Scene").GetComponent<UIVariables>().SaveAll();
    }
}
