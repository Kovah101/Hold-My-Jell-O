using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private bool finished = false;
    public float deadZone = -7.5f;

    void Start()
    {
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);

    }

    private void OnDisable()
    {
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.RemoveListener(OnResetGame);
    }

    void Update()
    {
       if (finished == false)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
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
            GameEventSystem.Instance.FinishGameEvent.Invoke();
        }
    }

    private void OnFinishGame()
    {
        finished = true;
    }

    private void OnResetGame()
    {
        Destroy(gameObject);
    }
    
}
