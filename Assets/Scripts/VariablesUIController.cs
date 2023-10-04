using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariablesUIController : MonoBehaviour
{
    public TMP_Text difficultyText;
    public TMP_Text enemySpeedText;
    public TMP_Text spawnTimerText;
    void Start()
    {
        GameEventSystem.Instance.UpdateDifficulty.AddListener(UpdateDifficulty);
        GameEventSystem.Instance.UpdateEnemySpeed.AddListener(UpdateEnemySpeed);
        GameEventSystem.Instance.UpdateSpawnTimer.AddListener(UpdateSpawnTimer);
    }

    private void OnDisable()
    {
        GameEventSystem.Instance.UpdateDifficulty.RemoveListener(UpdateDifficulty);
        GameEventSystem.Instance.UpdateEnemySpeed.RemoveListener(UpdateEnemySpeed);
        GameEventSystem.Instance.UpdateSpawnTimer.RemoveListener(UpdateSpawnTimer);
    }

    private void UpdateDifficulty(float difficulty)
    {
        difficultyText.text = "Difficulty: " + difficulty.ToString("F2");
    }

    private void UpdateEnemySpeed(float speed)
    {
        enemySpeedText.text = "Enemy Speed: " + speed.ToString("F2");
    }

    private void UpdateSpawnTimer(float timer)
    {
        spawnTimerText.text = "Spawn Timer: " + timer.ToString("F2");
    }


}
