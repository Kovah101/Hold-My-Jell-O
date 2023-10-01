using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject gameStartScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);

    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
    }

    private void OnStartGame()
    {
        Debug.Log("Game Started");
        gameStartScreen.SetActive(false);
    }

    private void OnFinishGame()
    {
        Debug.Log("Game Finished - setting game over menu to active");
        gameOverMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        GameEventSystem.Instance.ResetGameEvent.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}
