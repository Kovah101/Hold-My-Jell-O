using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyHands;
    public float spawnRate = 2.5f;
    public float maxLeftPosition = -5.0f;
    public float maxRightPosition = 2.8f;
    public float minLeftPosition = -1.25f;
    public float minRightPosition = 1.25f;

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

        Vector3 newHandPosition = gameObject.transform.position;





        GameObject newHand = Instantiate(enemyHands[randomIndex], transform.position, Quaternion.identity);
        
        int randomFlip = Random.Range(0, 2);

        if (randomFlip == 1)
        {
            Debug.Log("Flipping hand");
            Vector3 newScale = newHand.transform.localScale;
            newScale.x *= -1;
            newHand.transform.localScale = newScale;
            newHand.transform.position = new Vector3(maxRightPosition, transform.position.y, 0);
        }
        else
        {
            Debug.Log("Not flipping hand");
            newHand.transform.position = new Vector3(maxLeftPosition, transform.position.y, 0);
        }

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
