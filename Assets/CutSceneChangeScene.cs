using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneChangeScene : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
