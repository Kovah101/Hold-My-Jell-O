using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyHandController : MonoBehaviour
{
    public float startingMoveSpeed = 3.5f;
    public float speedIncrease = 0.25f;

    private bool finished = false;
    public float deadZone = -6.5f;
    private AudioSource[] jellysplats;
    private bool soundOn;
    private int difficultyLevel = 0;
    private bool playerInvicible = false;

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

        startingMoveSpeed = PlayerPrefs.GetFloat("StartingSpeed", 1.75f);
        speedIncrease = PlayerPrefs.GetFloat("SpeedIncrement", 0.25f);
        playerInvicible = PlayerPrefs.GetInt("Invincibility") == 1 ? true : false;

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

            if(playerInvicible == false)
            {
                GameEventSystem.Instance.FinishGameEvent.Invoke();
            }
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
        startingMoveSpeed += difficultyLevel * speedIncrease;
        GameEventSystem.Instance.UpdateEnemySpeed.Invoke(startingMoveSpeed);
    }

}
