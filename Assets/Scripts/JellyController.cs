using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyController : MonoBehaviour
{
    public float deadZone = -7.5f;

    private GameObject jellyCenter;

    void Start()
    {
        jellyCenter = gameObject.transform.GetChild(0).gameObject;
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
        int jellyBoneCount = gameObject.transform.childCount;
        for (int i = 0; i < jellyBoneCount; i++)
        {
            Rigidbody2D jellyBoneBody = gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
            jellyBoneBody.WakeUp();
        }
    }

    private void OnFinishGame()
    {
        Destroy(gameObject);
    }
}
