using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject backgroundMusic;
    public GameObject buttonClickSound;

    private bool musicOn = true;
    private bool soundOn = true;

    void Start()
    {
        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        soundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;

        backgroundMusic.GetComponent<AudioSource>().mute = !musicOn;
        buttonClickSound.GetComponent<AudioSource>().mute = !soundOn;
    }

}
