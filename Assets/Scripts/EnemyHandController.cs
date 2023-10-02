using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandController : MonoBehaviour
{
    public float startingMoveSpeed = 3.5f;


    private bool finished = false;
    public float deadZone = -6.5f;
    private AudioSource[] jellysplats;
    private bool soundOn;
    private int difficultyLevel = 0;

    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);

        jellysplats = GetComponents<AudioSource>();
        soundOn = PlayerPrefs.GetInt("SoundOn") == 1 ? true : false;


        foreach (var item in jellysplats)
        {
            item.mute = !soundOn;
        }

        difficultyLevel = PlayerPrefs.GetInt("DifficultyLevel", 0);
        IncreaseSpeed(difficultyLevel);

    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.RemoveListener(OnResetGame);
    }

    void Update()
    {
        if (finished == false)
        {
            transform.position += Vector3.down * startingMoveSpeed * Time.deltaTime;
        }

        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Jelly")
        {
            jellysplats[Random.Range(0, jellysplats.Length)].Play();

            //GameEventSystem.Instance.FinishGameEvent.Invoke();
        }
    }

    private void OnStartGame()
    {
    }

    private void OnFinishGame()
    {
        finished = true;
    }

    private void OnResetGame()
    {
        Destroy(gameObject);
        finished = false;
    }

    private void IncreaseSpeed(int difficultyLevel)
    {
        startingMoveSpeed += difficultyLevel * 0.5f;
    }

}
