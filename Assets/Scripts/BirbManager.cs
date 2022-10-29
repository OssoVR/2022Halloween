using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BirbManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject endScreen;

    public GameObject spawnLeft;
    public GameObject spawnRight;

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI currentScoreText;
    int score;

    bool playGame = false;

    private void Start()
    {
        StartScreen();
    }

    private void Update()
    {
        if (playGame)
        {
            spawnLeft.SetActive(true);
            spawnRight.SetActive(true);
        }
        else
        {
            spawnLeft.SetActive(false);
            spawnRight.SetActive(false);
        }

        currentScoreText.text = "Score: " + score;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void StartScreen()
    {
        startScreen.SetActive(true);
        endScreen.SetActive(false);
        playGame = false;
        score = 0;
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        endScreen.SetActive(false);
        playGame = true;
        score = 0;
    }

    public void EndGame()
    {
        startScreen.SetActive(false);
        endScreen.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        playGame = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore(int point)
    {
        score += point;
    }
}
