using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject MusicOn;
    public GameObject MusicOff;
    public GameObject BackgroundMusic;
    public GameObject SoundOn;
    public GameObject SoundOff;
    public GameObject ButtonClickSound;


    private bool musicOn = true;
    private bool soundOn = true;

    private void Start()
    {
        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        ToggleMusic(musicOn);

        soundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        ToggleSound(soundOn);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMusic(bool musicOn)
    {
        MusicOn.GetComponent<CanvasGroup>().alpha = musicOn ? 1f : 0.5f;
        MusicOff.GetComponent<CanvasGroup>().alpha = musicOn ? 0.5f : 1f;

        BackgroundMusic.GetComponent<AudioSource>().mute = !musicOn;

        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSound(bool soundOn)
    {
        SoundOn.GetComponent<CanvasGroup>().alpha = soundOn ? 1f : 0.5f;
        SoundOff.GetComponent<CanvasGroup>().alpha = soundOn ? 0.5f : 1f;

        ButtonClickSound.GetComponent<AudioSource>().mute = !soundOn;

        PlayerPrefs.SetInt("SoundOn", soundOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
