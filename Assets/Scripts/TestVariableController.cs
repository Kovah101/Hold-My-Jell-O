using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestVariableController : MonoBehaviour
{
    public Slider scoreDifficulty;
    public TMP_Text scoreDifficultyText;
    public Slider speedDifficulty;
    public TMP_Text speedDifficultyText;
    public Slider timerDifficulty;
    public TMP_Text timerDifficultyText;
    public Slider spawnDifficulty;
    public TMP_Text spawnDifficultyText;
    public Slider startingSpeed;
    public TMP_Text startingSpeedText;
    public Slider speedIncrement;
    public TMP_Text speedIncrementText;
    public Slider startingSpawnTimer;
    public TMP_Text startingSpawnTimerText;
    public Slider spawnTimerDecrement;
    public TMP_Text spawnTimerDecrementText;
    public Slider spawnPatternIncrement;
    public TMP_Text spawnPatternIncrementText;
    public Toggle invincibility;
    public TMP_Text invincibilityText;
    public Toggle variablesUi;
    public TMP_Text variablesUiText;

    private int scoreDifficultyValue;
    private int speedDifficultyValue;
    private int timerDifficultyValue;
    private int spawnDifficultyValue;
    private float startingSpeedValue;
    private float speedIncrementValue;
    private float startingSpawnTimerValue;
    private float spawnTimerDecrementValue;
    private int spawnPatternIncrementValue;
    private bool invincibilityValue;
    private bool variablesUiValue;


    // Start is called before the first frame update
    void Start()
    {
        scoreDifficultyValue = PlayerPrefs.GetInt("ScoreDifficulty", 100);
        SetScoreDifficulty((float)scoreDifficultyValue);
        
        speedDifficultyValue = PlayerPrefs.GetInt("SpeedDifficulty", 2);
        SetSpeedDifficulty((float)speedDifficultyValue);

        timerDifficultyValue = PlayerPrefs.GetInt("TimerDifficulty", 3);
        SetTimerDifficulty((float)timerDifficultyValue);

        spawnDifficultyValue = PlayerPrefs.GetInt("SpawnDifficulty", 5);
        SetSpawnDifficulty((float)spawnDifficultyValue);

        startingSpeedValue = PlayerPrefs.GetFloat("StartingSpeed", 1.75f);
        SetStartingSpeed(startingSpeedValue);

        speedIncrementValue = PlayerPrefs.GetFloat("SpeedIncrement", 0.25f);
        SetSpeedIncrement(speedIncrementValue);

        startingSpawnTimerValue = PlayerPrefs.GetFloat("StartingSpawnTimer", 3f);
        SetStartingSpawnTimer(startingSpawnTimerValue);

        spawnTimerDecrementValue = PlayerPrefs.GetFloat("SpawnTimerDecrement", 0.25f);
        SetSpawnTimerDecrement(spawnTimerDecrementValue);

        spawnPatternIncrementValue = PlayerPrefs.GetInt("SpawnPatternIncrement", 1);
        SetSpawnPatternIncrement((float)spawnPatternIncrementValue);

        invincibilityValue = PlayerPrefs.GetInt("Invincibility", 0) == 1 ? true : false;
        SetInvincibility(invincibilityValue);

        variablesUiValue = PlayerPrefs.GetInt("VariablesUi", 0) == 1 ? true : false;
        SetVariablesUi(variablesUiValue);

    }

    public void SetScoreDifficulty(float value)
    {
        scoreDifficultyValue = (int)value;
        scoreDifficultyText.text = scoreDifficultyValue.ToString();
        scoreDifficulty.value = scoreDifficultyValue;
        PlayerPrefs.SetInt("ScoreDifficulty", scoreDifficultyValue);
        PlayerPrefs.Save();
    }

    public void SetSpeedDifficulty(float value)
    {
        speedDifficultyValue = (int)value;
        speedDifficultyText.text = speedDifficultyValue.ToString();
        speedDifficulty.value = speedDifficultyValue;
        PlayerPrefs.SetInt("SpeedDifficulty", speedDifficultyValue);
        PlayerPrefs.Save();
    }

    public void SetTimerDifficulty(float value)
    {
        timerDifficultyValue = (int)value;
        timerDifficultyText.text = timerDifficultyValue.ToString();
        timerDifficulty.value = timerDifficultyValue;
        PlayerPrefs.SetInt("TimerDifficulty", timerDifficultyValue);
        PlayerPrefs.Save();
    }

    public void SetSpawnDifficulty(float value)
    {
        spawnDifficultyValue = (int)value;
        spawnDifficultyText.text = spawnDifficultyValue.ToString();
        spawnDifficulty.value = spawnDifficultyValue;
        PlayerPrefs.SetInt("SpawnDifficulty", spawnDifficultyValue);
        PlayerPrefs.Save();
    }

    public void SetStartingSpeed(float value)
    {
        startingSpeedValue = value;
        startingSpeedText.text = startingSpeedValue.ToString("F2");
        startingSpeed.value = startingSpeedValue;
        PlayerPrefs.SetFloat("StartingSpeed", startingSpeedValue);
        PlayerPrefs.Save();
    }

    public void SetSpeedIncrement(float value)
    {
        speedIncrementValue = value;
        speedIncrementText.text = speedIncrementValue.ToString("F2");
        speedIncrement.value = speedIncrementValue;
        PlayerPrefs.SetFloat("SpeedIncrement", speedIncrementValue);
        PlayerPrefs.Save();
    }

    public void SetStartingSpawnTimer(float value)
    {
        startingSpawnTimerValue = value;
        startingSpawnTimerText.text = startingSpawnTimerValue.ToString("F2");
        startingSpawnTimer.value = startingSpawnTimerValue;
        PlayerPrefs.SetFloat("StartingSpawnTimer", startingSpawnTimerValue);
        PlayerPrefs.Save();
    }

    public void SetSpawnTimerDecrement(float value)
    {
        spawnTimerDecrementValue = value;
        spawnTimerDecrementText.text = spawnTimerDecrementValue.ToString("F2");
        spawnTimerDecrement.value = spawnTimerDecrementValue;
        PlayerPrefs.SetFloat("SpawnTimerDecrement", spawnTimerDecrementValue);
        PlayerPrefs.Save();
    }

    public void SetSpawnPatternIncrement(float value)
    {
        spawnPatternIncrementValue = (int)value;
        spawnPatternIncrementText.text = spawnPatternIncrementValue.ToString();
        spawnPatternIncrement.value = spawnPatternIncrementValue;
        PlayerPrefs.SetInt("SpawnPatternIncrement", spawnPatternIncrementValue);
        PlayerPrefs.Save();
    }

    public void SetInvincibility(bool value)
    {
        invincibilityValue = value;
        invincibility.isOn = invincibilityValue;
        invincibilityText.text = invincibilityValue.ToString();
        PlayerPrefs.SetInt("Invincibility", invincibilityValue ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetVariablesUi(bool value)
    {
        variablesUiValue = value;
        variablesUi.isOn = variablesUiValue;
        variablesUiText.text = variablesUiValue.ToString();
        PlayerPrefs.SetInt("VariablesUi", variablesUiValue ? 1 : 0);
        PlayerPrefs.Save();
    }
}
