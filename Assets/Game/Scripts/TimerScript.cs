using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text countdown;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdown.text = "Next match in... " + Mathf.Ceil(timeRemaining).ToString() + " seconds";
            }
            else
            {
                SceneManager.LoadScene(sceneName: "StartScene");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}