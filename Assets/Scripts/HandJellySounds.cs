using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJellySounds : MonoBehaviour
{
    public AudioSource[] jellysplats;

    private float timer = 0f;
    private float maxTimer = 1.5f;
    public void OnCollisionEnter2D(Collision2D collision)
    {
           if(timer > maxTimer)
            {
                PlayJellySplat(collision);
                timer = 0f;
            }        
    }

    private void PlayJellySplat(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jelly"))
        {
            jellysplats[Random.Range(0, jellysplats.Length)].Play();
        }
    }

    void Update()
    {
        if(timer < maxTimer)
        {
            timer += Time.deltaTime;
        }
    }
    
}
