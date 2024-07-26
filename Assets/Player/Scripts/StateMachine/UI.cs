using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject player;
    private Collect objRecup;
    public GameObject victoire;
    public GameObject defaite;
    public TMP_Text timerText;
    public float initialTime = 60f; // Temps initial en secondes
    private float currentTime;
    private float timePast;
    public string sceneName;
    public string sceneName2;
    public TMP_Text tempsMis;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        if (player != null)
        {
            objRecup = player.GetComponent<Collect>();
        }

        // Initialiser le timer
        currentTime = initialTime;
        UpdateTimerText();
    }
    void UpdateTimerText()
    {
        // Calculer les minutes et les secondes
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Formatage du texte du timer
        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    public void ShowVictoire()
    {
        Time.timeScale = 0f;
        // Calculer les minutes et les secondes
        int minutes = Mathf.FloorToInt(timePast / 60);
        int seconds = Mathf.FloorToInt(timePast % 60);

        // Formatage du texte du timer
        tempsMis.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        victoire.SetActive(true);
    }

    public void ShowDefaite()
    {
        Time.timeScale = 0f;
        defaite.SetActive(true);
    }


    public void MenuPrincipal()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneName2);
    }

    // Update is called once per frame
    void Update()
    {
        if (objRecup.nbRamasser == 21)
        {
            ShowVictoire();
        }
        else if (currentTime <= 1)
        {
            ShowDefaite();
        }

        // Décrémenter le timer
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timePast += Time.deltaTime;
            UpdateTimerText();
        }

    }
}