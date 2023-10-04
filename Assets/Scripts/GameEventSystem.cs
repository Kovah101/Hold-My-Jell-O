using UnityEngine;
using UnityEngine.Events;

public class GameEventSystem : MonoBehaviour
{ 
    public static GameEventSystem Instance { get; private set; }

    public BasicGameEvent StartGameEvent = new BasicGameEvent();
    public BasicGameEvent ResetGameEvent = new BasicGameEvent();
    public BasicGameEvent FinishGameEvent = new BasicGameEvent();
    public BasicGameEvent DecreaseSpawnTimer = new BasicGameEvent();
    public BasicGameEvent IncreaseSpawnPattern = new BasicGameEvent();
    public TestGameEvent UpdateDifficulty = new TestGameEvent();
    public TestGameEvent UpdateEnemySpeed = new TestGameEvent();
    public TestGameEvent UpdateSpawnTimer = new TestGameEvent();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate NodeEventSystem instance found. Removing duplicate.");
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class BasicGameEvent : UnityEvent { }
public class TestGameEvent: UnityEvent<float> { }
