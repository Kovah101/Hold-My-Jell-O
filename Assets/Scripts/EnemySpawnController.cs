using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyHands;
    public float spawnRate = 2.5f;

    private bool started = false;
    private float timer = 0f;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate && started == true)
        {
            timer += Time.deltaTime;
        }
        else if ( started == true )
        {
            SpawnHand();
            timer = 0f;
        }
    }

    private void SpawnHand()
    {
        int randomIndex = Random.Range(0, enemyHands.Length);

        // add random flipping of the hand in the x axis
        int randomFlip = Random.Range(0, 2);
        if (randomFlip == 1)
        {
            enemyHands[randomIndex].transform.localScale = new Vector3(-1f, 1f, 1f);
        }



        Instantiate(enemyHands[randomIndex], transform.position, Quaternion.identity);
    }

    private void OnStartGame()
    {
        started = true;
        SpawnHand();
    }

    private void OnFinishGame()
    {

    }

    private void OnResetGame()
    {
        started = false;
        timer = 0f;
    }
}
