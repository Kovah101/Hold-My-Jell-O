using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyController : MonoBehaviour
{
    public float deadZone = -7.5f;

    private GameObject jellyCenter;


    void Start()
    {
        jellyCenter = GameObject.Find("JellyCenter");
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        
    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
    }
    

    void Update()
    {
        if(jellyCenter.transform.position.y < deadZone)
        {
            GameEventSystem.Instance.FinishGameEvent.Invoke();
        }
    }

    private void OnStartGame()
    {
        Debug.Log("JellyController: OnStartGame");
        // wake up all rigidbody bones
    }

    private void OnFinishGame()
    {
        Destroy(gameObject);
    }
}
