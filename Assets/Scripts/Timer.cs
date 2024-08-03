using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Asegúrate de incluir este namespace

public class Timer : MonoBehaviour
{
    public Text timerText; 
    public float timeRemaining = 60; 

    private bool timerIsRunning = false;

    private void Start()
    {
        timerIsRunning = true; 
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                LoadEndRoundScreen(); // Llama a la función para cargar la pantalla de fin de ronda
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LoadEndRoundScreen()
    {
        SceneManager.LoadScene("FinRonda");
    }
}