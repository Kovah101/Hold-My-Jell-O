using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandController : MonoBehaviour
{
    public float startingMoveSpeed = 2f;

   // private bool started = false;
    private bool finished = false;
    public float deadZone = -6.5f;

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
           // GameEventSystem.Instance.FinishGameEvent.Invoke();
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
