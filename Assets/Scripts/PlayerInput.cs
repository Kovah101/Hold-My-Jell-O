using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject player;

    private Camera mainCamera;
    private bool hasStarted = false;
    private Rigidbody2D handRigidbody;
    [SerializeField] private float yOffset = 0.3f;
    [SerializeField] private float moveSpeed = 10f;

    // TODO : add wall colliders to stop hand and jelly going off screen, add reset trigger when jelly falls off screen


    void Start()
    {
        mainCamera = Camera.main;
        handRigidbody = player.GetComponent<Rigidbody2D>();
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

            if (hasStarted == true)
            {
                var touchPos = mainCamera.ScreenToWorldPoint(Input.touches[0].position);
                var desiredPos = new Vector3(touchPos.x, touchPos.y + yOffset, touchPos.z);

                Vector2 movedirection = (desiredPos - player.transform.position).normalized;

                handRigidbody.velocity = new Vector2(movedirection.x * moveSpeed, movedirection.y * moveSpeed);
                handRigidbody.angularVelocity = 0f;

            } 
            else
            {
                handRigidbody.velocity = Vector2.zero;
                handRigidbody.angularVelocity = 0f;
            }
        }
        else
        { // if removed then hand keeps moving, could lerp to 0?
            handRigidbody.velocity = Vector2.zero;
            handRigidbody.angularVelocity = 0f;
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
