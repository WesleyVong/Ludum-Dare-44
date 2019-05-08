using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCommands : MonoBehaviour
{
    public void Quit()
    {
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

    public void loadScene(int sceneNum)
    {
        Debug.Log("Loading Scene");
        try
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posX", GameObject.FindGameObjectWithTag("Player").transform.position.x);
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posY", GameObject.FindGameObjectWithTag("Player").transform.position.y);
        }
        catch
        {
            Debug.Log("No Player Found");
        }

        SceneManager.LoadScene(sceneNum);
    }
    public void loadScene(string sceneName)
    {
        Debug.Log("Loading Scene");
        try
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posX", GameObject.FindGameObjectWithTag("Player").transform.position.x);
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "-posY", GameObject.FindGameObjectWithTag("Player").transform.position.y);
        }
        catch
        {
            Debug.Log("No Player Found");
        }
        SceneManager.LoadScene(sceneName);
    }
}
