using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{


    private void Start()
    {
        //GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
       // GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);
    }
    

    private void OnDisable()
    {
        //GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        //GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.RemoveListener(OnResetGame);
    }

    public void ReturnToMenu()
    {
        GameEventSystem.Instance.ResetGameEvent.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    public void Again()
    {
        GameEventSystem.Instance.ResetGameEvent.Invoke();
    }

    //private void OnFinishGame()
    //{
    //    Debug.Log("Game Over - Show the menu");
    //    gameObject.SetActive(true);
    //}

    private void OnResetGame()
    {
        gameObject.SetActive(false);
    }
}
