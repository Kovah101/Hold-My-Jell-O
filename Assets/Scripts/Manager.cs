using System;
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
    public GameObject variablesUI;
    public TMP_Text liveScore;
    public TMP_Text finalScore;
    public TMP_Text highScore;
    public float scoreMultiplier = 1f;

    private float difficultyBarrier;
    private bool started = false;
    private float startTime;
    private float currentTime;
    private float score;
    private float currentHighScore;
    private int difficultyLevel = 0;
    private int previousDifficultyLevel;
    private int speedDifficulty;
    private int spawnTimeDifficulty;
    private int spawnPatternDifficulty;
    private float startingSpeed;
    private bool showVariablesUi = false;
    private bool invicible;

    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);

        currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);
        difficultyBarrier = PlayerPrefs.GetInt("ScoreDifficulty", 100);
        speedDifficulty = PlayerPrefs.GetInt("SpeedDifficulty", 2);
        spawnTimeDifficulty = PlayerPrefs.GetInt("TimerDifficulty", 3);
        spawnPatternDifficulty = PlayerPrefs.GetInt("SpawnDifficulty", 5);
        startingSpeed = PlayerPrefs.GetFloat("StartingSpeed", 1.75f);
        invicible = PlayerPrefs.GetInt("Invincibility", 0) == 1 ? true : false;

        difficultyLevel = 0;
        PlayerPrefs.SetInt("DifficultyLevel", difficultyLevel);
        PlayerPrefs.Save();
        GameEventSystem.Instance.UpdateDifficulty.Invoke(difficultyLevel);
        GameEventSystem.Instance.UpdateEnemySpeed.Invoke(startingSpeed);


        showVariablesUi = PlayerPrefs.GetInt("VariablesUi", 1) == 1 ? true : false;
        variablesUI.SetActive(showVariablesUi);


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

            difficultyLevel = (int)score / (int)difficultyBarrier;
            GameEventSystem.Instance.UpdateDifficulty.Invoke(difficultyLevel);
            if(difficultyLevel != previousDifficultyLevel)
            {
                IncreaseDifficulty(difficultyLevel);
                previousDifficultyLevel = difficultyLevel;
            }
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
        if (score > currentHighScore && !invicible)
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
        difficultyLevel = 0;
        previousDifficultyLevel = 0;
        PlayerPrefs.SetInt("DifficultyLevel", difficultyLevel);
        PlayerPrefs.Save();
        GameEventSystem.Instance.UpdateDifficulty.Invoke(difficultyLevel);
    }

    public void ReturnToMenu()
    {
        GameEventSystem.Instance.ResetGameEvent.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    private void IncreaseDifficulty(int difficultyLevel)
    {
        if (difficultyLevel < 1)
        {
            return;
        }

        if (difficultyLevel % speedDifficulty == 0)
        {
            PlayerPrefs.SetInt("DifficultyLevel", difficultyLevel);
            PlayerPrefs.Save();
        }

        if (difficultyLevel % spawnTimeDifficulty == 0)
        {
            GameEventSystem.Instance.DecreaseSpawnTimer.Invoke();
        }

        if (difficultyLevel % spawnPatternDifficulty == 0)
        {
            GameEventSystem.Instance.IncreaseSpawnPattern.Invoke();
        }
    }
}
