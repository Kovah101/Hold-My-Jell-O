using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject gameStartScreen;
    public TMP_Text liveScore;
    public TMP_Text finalScore;
    public TMP_Text highScore;
    public double scoreMultiplier = 1;
    public double difficultyBarrier = 120;

    private bool started = false;
    private double startTime;
    private double currentTime;
    private double score;
    private float currentHighScore;
    private double difficultyLevel = 0.0;
    private int speedDifficulty;
    private int spawnTimeDifficulty;
    private int spawnPatternDifficulty;

    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);

        currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);
        difficultyBarrier = PlayerPrefs.GetFloat("ScoreDifficulty", 120);
        speedDifficulty = PlayerPrefs.GetInt("SpeedDifficulty", 2);
        spawnTimeDifficulty = PlayerPrefs.GetInt("TimerDifficulty", 3);
        spawnPatternDifficulty = PlayerPrefs.GetInt("SpawnDifficulty", 5);

    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.RemoveListener(OnResetGame);
    }

    void Update()
    {
        if (started == true && gameOverMenu.activeSelf == false)
        {
            currentTime = System.Environment.TickCount;
            score = (currentTime - startTime) * scoreMultiplier;
            liveScore.text = score.ToString("F0");

            difficultyLevel = score / difficultyBarrier;
            IncreaseDifficulty(difficultyLevel);

        }
    }

    private void OnStartGame()
    {
        gameStartScreen.SetActive(false);
        started = true;
        startTime = System.Environment.TickCount;
    }

    private void OnFinishGame()
    {
        started = false;
        gameOverMenu.SetActive(true);
        finalScore.text = "Score: " + score.ToString("F0");
        if (score > currentHighScore)
        {
            currentHighScore = (float)score;
            PlayerPrefs.SetFloat("HighScore", (float)score);
            PlayerPrefs.Save();
        }
        highScore.text = "High Score: " + currentHighScore.ToString("F0");
    }

    private void OnResetGame()
    {
        liveScore.text = "0";
        
    }

    public void ReturnToMenu()
    {
        GameEventSystem.Instance.ResetGameEvent.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    private void IncreaseDifficulty(double difficultyLevel)
    {
        if (difficultyLevel < 1)
        {
            return;
        }

        if (difficultyLevel % speedDifficulty == 0)
        {
            Debug.Log("Difficulty Level: " + difficultyLevel + ", Increasing Speed");
            PlayerPrefs.SetInt("DifficultyLevel", (int)difficultyLevel);
            PlayerPrefs.Save();
        }

        if (difficultyLevel % spawnTimeDifficulty == 0)
        {
            Debug.Log("Difficulty Level: " + difficultyLevel + ", Decreasing SpawnTime");
            GameEventSystem.Instance.DecreaseSpawnTimer.Invoke();
        }

        if (difficultyLevel % spawnPatternDifficulty == 0)
        {
            Debug.Log("Difficulty Level: " + difficultyLevel + ", Increasing SpawnPattern");
            GameEventSystem.Instance.IncreaseSpawnPattern.Invoke();
        }
    }   
}
