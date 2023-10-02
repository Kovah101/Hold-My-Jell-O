using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    public AudioSource wallBounce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jelly"))
        {
            Debug.Log("Jelly Bone Hit Wall");
            wallBounce.Play();           
        }
    }
}