using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    public GameObject player;

    private Camera mainCamera;
    private bool hasStarted = false;


    void Start()
    {
        mainCamera = Camera.main;
        GameEventSystem.Instance.StartGameEvent.AddListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.AddListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.AddListener(OnResetGame);
    }

    private void OnDisable()
    {
        GameEventSystem.Instance.StartGameEvent.RemoveListener(OnStartGame);
        GameEventSystem.Instance.FinishGameEvent.RemoveListener(OnFinishGame);
        GameEventSystem.Instance.ResetGameEvent.RemoveListener(OnResetGame);
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.touches[0].position);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.CompareTag("Player") && hasStarted == false)
            {
                GameEventSystem.Instance.StartGameEvent.Invoke();
            }

            if(hasStarted == true)
            {
                // convert the touch position to world position
                var touchPos = mainCamera.ScreenToWorldPoint(Input.touches[0].position);

                player.transform.position = new Vector3(touchPos.x, touchPos.y, 0);
            }
        }
    }

    private void OnStartGame()
    {
        //activate rigidbody and hide starting ui
        hasStarted = true;
    }

    private void OnFinishGame()
    {
        //show high score and ui
        hasStarted = false;
    }

    private void OnResetGame()
    {
        // resetplayer position and all UI
        hasStarted = false;
    }
}
