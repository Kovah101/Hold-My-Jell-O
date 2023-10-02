using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyHands;
    public float spawnTimer = 7f;
    public float maxLeftPosition = -2.85f;
    public float maxRightPosition = 2.85f;
    public float minLeftPosition = -1.12f;
    public float minRightPosition = 1.12f;

    private bool started = false;
    private bool flipped = false;
    private float timer = 0f;

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
        if (timer < spawnTimer && started == true)
        {
            timer += Time.deltaTime;
        }
        else if ( started == true )
        {
            SpawnHand(first: false);
            timer = 0f;
        }
    }

    private void SpawnHand(bool first)
    {
        int randomIndex = Random.Range(0, enemyHands.Length);

        GameObject newHand = Instantiate(enemyHands[randomIndex], transform.position, Quaternion.identity);
        
        float xPosition;

        if (flipped == true)
        {
            Vector3 newScale = newHand.transform.localScale;
            newScale.x *= -1;
            newHand.transform.localScale = newScale;

            if(first)
            {
                xPosition = minLeftPosition;
            } 
            else
            {
                xPosition = Random.Range(minLeftPosition, maxLeftPosition);
            }

            newHand.transform.position = new Vector3(xPosition, transform.position.y, 0);
            flipped = false;
        }
        else
        {
            if(first)
            {
                xPosition = minRightPosition;
            }
            else
            {
                xPosition = Random.Range(minRightPosition, maxRightPosition);
            }

            newHand.transform.position = new Vector3(xPosition, transform.position.y, 0);
            flipped = true;
        }

    }

    private void OnStartGame()
    {
        started = true;
        SpawnHand(first: true);
    }

    private void OnFinishGame()
    {
        started = false;
    }

    private void OnResetGame()
    {
        started = false;
        timer = 0f;
    }
}
