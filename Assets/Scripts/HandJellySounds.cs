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

        if(gameObject.CompareTag("Player"))
        {
           if(timer > maxTimer)
            {
                PlayJellySplat(collision);
                timer = 0f;
            }

        } 
        else if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hand Hit Jelly");
            PlayJellySplat(collision);
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
        if(gameObject.CompareTag("Player") && timer < maxTimer)
        {
            Debug.Log(timer);
            timer += Time.deltaTime;
        }
    }
    
}
