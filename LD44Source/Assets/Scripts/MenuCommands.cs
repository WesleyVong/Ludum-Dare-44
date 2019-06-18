using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCommands : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetString("Music","true") == "true")
        {
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
    }
    public void Quit()
    {
        try
        {
            Save();
        }
        catch
        {
            Debug.Log("Save Function Error");
        }
        Application.Quit();
    }

    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Overworld");
    }

    public void ToggleMusic()
    {
        if (PlayerPrefs.GetString("Music") == "false")
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetString("Music", "true");
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            PlayerPrefs.SetString("Music", "false");
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

    public void loadLastScene()
    {
        Debug.Log("Loading Scene");
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene", "Overworld"));
    }

    public void Save()
    {
        // Save Player Position
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posX", GameObject.FindGameObjectWithTag("Player").transform.position.x);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posY", GameObject.FindGameObjectWithTag("Player").transform.position.y);

        Debug.Log(SceneManager.GetActiveScene().name + PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "-posX"));

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

        if (SceneManager.GetActiveScene().name != "Start Scene")
        {
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        }

    }
}
