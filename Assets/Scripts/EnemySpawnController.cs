using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyHands;
    public GameObject[] enemyDoubleHands;
    public float spawnTimer = 3f;
    public float spawnTimerDecrement = 0.25f;
    public float maxLeftPosition = -2.85f;
    public float maxRightPosition = 2.85f;
    public float minLeftPosition = -1.12f;
    public float minRightPosition = 1.12f;
    public float minDoublePosition = -1.3f;
    public float maxDoublePosition = 1.3f;
    public int randomHands = 4;

    private bool started = false;
    private bool flipped = false;
    private float timer = 0f;

    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);
        GameEventSystem.Instance.IncreaseSpawnPattern.AddListener(IncreaseSpawnPattern);
        GameEventSystem.Instance.DecreaseSpawnTimer.AddListener(DecreaseSpawnTimer);
    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.RemoveListener(OnResetGame);
        GameEventSystem.Instance.IncreaseSpawnPattern.RemoveListener(IncreaseSpawnPattern);
        GameEventSystem.Instance.DecreaseSpawnTimer.RemoveListener(DecreaseSpawnTimer);
    }

    void Update()
    {
        if (timer < spawnTimer && started == true)
        {
            timer += Time.deltaTime;
        }
        else if ( started == true )
        {
            int randomSpawn = Random.Range(0, randomHands);

            if (randomSpawn <= 1)
            {
                SpawnHand(first: false);
            } 
            else
            {
                SpawnHands();
            }

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

    private void SpawnHands()
    {
        int randomIndex = Random.Range(0, enemyDoubleHands.Length);

        GameObject newHand = Instantiate(enemyDoubleHands[randomIndex], transform.position, Quaternion.identity);

        float xPosition = Random.Range(minDoublePosition, maxDoublePosition);

        newHand.transform.position = new Vector3(xPosition, transform.position.y, 0);
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

    private void IncreaseSpawnPattern()
    {
        Debug.Log("Increase Spawn Pattern - more doubles should appear");
        randomHands++;
    }

    private void DecreaseSpawnTimer()
    {
        Debug.Log("Decrease Spawn Timer - should spawn more often");
        spawnTimer -= spawnTimerDecrement;
    }
}
