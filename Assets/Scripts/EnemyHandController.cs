using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandController : MonoBehaviour
{
    public float startingMoveSpeed = 3.5f;
    

    // private bool started = false;
    private bool finished = false;
    public float deadZone = -6.5f;
    private AudioSource[] jellysplats;

    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);

        jellysplats = GetComponents<AudioSource>();

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
            Debug.Log("Jelly Bone Hit Enemy");
            jellysplats[Random.Range(0, jellysplats.Length)].Play();

            GameEventSystem.Instance.FinishGameEvent.Invoke();
        }
    }

    private void OnStartGame()
    {
       // started = true;
    }

    private void OnFinishGame()
    {
        finished = true;
    }

    private void OnResetGame()
    {
        Destroy(gameObject);
       // started = false;
        finished = false;
    }

    private void FlipEnemyHand()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
    }
}
