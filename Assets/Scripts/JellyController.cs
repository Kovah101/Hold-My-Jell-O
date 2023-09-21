using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyController : MonoBehaviour
{
    public float deadZone = -7.5f;


    void Start()
    {
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);

        
    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
    }
    

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deadZone)
        {
            GameEventSystem.Instance.FinishGameEvent.Invoke();
        }
    }

    private void OnStartGame()
    {
        Debug.Log("JellyController: OnStartGame");
    }
}
