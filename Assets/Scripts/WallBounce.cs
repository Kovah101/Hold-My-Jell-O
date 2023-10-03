using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    public AudioSource wallBounce;

    private bool soundOn;

    private void Start()
    {
        soundOn = PlayerPrefs.GetInt("SoundOn") == 1 ? true : false;

        wallBounce.mute = !soundOn;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jelly"))
        {
            wallBounce.Play();           
        }
    }
}
