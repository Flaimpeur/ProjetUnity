using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    public string sceneName2;

    public void Play()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Credit()
    {
        SceneManager.LoadScene(sceneName2);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
