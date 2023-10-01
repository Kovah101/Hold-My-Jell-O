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
    public double scoreMultiplier = 1;

    private double startTime;
    private double currentTime;
    private double score;

    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);

    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.RemoveListener(OnResetGame);
    }

    void Update()
    {
        if (gameStartScreen.activeSelf == false && gameOverMenu.activeSelf == false)
        {
            currentTime = System.Environment.TickCount;
            score = (currentTime - startTime) * scoreMultiplier;
           // score += Time.deltaTime * scoreMultiplier; // score in multiples of seconds
            liveScore.text = score.ToString("F0");
        }
    }

    private void OnStartGame()
    {
        Debug.Log("Game Started");
        gameStartScreen.SetActive(false);
        startTime = System.Environment.TickCount;
    }

    private void OnFinishGame()
    {
        Debug.Log("Game Finished - setting game over menu to active");
        gameOverMenu.SetActive(true);
        finalScore.text = "Score: " + score.ToString("F0");
    }

    private void OnResetGame()
    {
        Debug.Log("Game Reset");
        liveScore.text = "0";
        
    }

    public void ReturnToMenu()
    {
        GameEventSystem.Instance.ResetGameEvent.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}
