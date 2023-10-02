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
    public GameObject backgroundMsuic;
    public TMP_Text liveScore;
    public TMP_Text finalScore;
    public TMP_Text highScore;
    public double scoreMultiplier = 1;

    private bool started = false;
    private double startTime;
    private double currentTime;
    private double score;
    private float currentHighScore;
    private bool musicOn = true;

    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);

        currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);
        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;

        backgroundMsuic.GetComponent<AudioSource>().mute = !musicOn;

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
}
