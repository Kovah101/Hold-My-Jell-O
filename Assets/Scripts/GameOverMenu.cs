using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{


    private void Start()
    {
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);
    }
    

    private void OnDisable()
    {
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
        gameObject.SetActive(false);
    }

    private void OnResetGame()
    {
        gameObject.SetActive(false);
    }
}
