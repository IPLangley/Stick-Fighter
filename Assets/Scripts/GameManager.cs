using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject GameOverUI;

    public Text timeText;
    public Text loserText;
    private float timeRemaining;
    public float timeLimit = 150;
    private bool timerIsRunning = false;
    void Start()
    {
        GameOverUI.SetActive(false);
        timeRemaining = timeLimit;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(timeRemaining);
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        if (GameOverUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.Escape))
            {
                Rematch();
                Time.timeScale = 1;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.Space))
            {
                GoToMainMenu();
                Time.timeScale = 1;
            }
        }
    }


    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void GameOver(int winner)
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
        switch (winner)
        {
            case 1:
                loserText.text = "Player 1 Loses";
                break;
            case 2:
                loserText.text = "Player 2 Loses";
                break;
        }
    }

    private void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
