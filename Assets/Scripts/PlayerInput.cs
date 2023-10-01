using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject player;
    public GameObject jelly;

    private Camera mainCamera;
    private bool hasStarted = false;
    private Rigidbody2D handRigidbody;
    [SerializeField] private float yOffset = 0.3f;
    [SerializeField] private float moveSpeed = 10f;
    private Vector3 playerStartPosition = new Vector3(0.1f, -1f, 0f);
    private Vector3 jellyStartPosition = new Vector3(0f, 1f, 0f);

    // TODO : Add enemy hand prefabs, move them along the screen, add collision with triggers - if touching the jelly then finish game, destroy hands and reset
    // TODO : add enemy hand spawn patterns and increasing difficulty timers


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
    { // TODO: add flip player hand if screen is double tapped
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
        // hide starting ui
        Debug.Log("Game Started");
        hasStarted = true;
    }

    private void OnFinishGame()
    {
        //show high score and ui
        Debug.Log("Game Finished");
       // GameEventSystem.Instance.ResetGameEvent.Invoke();
    }

    private void OnResetGame()
    {
        // resetplayer position and all UI
        Debug.Log("Game Reset");
        hasStarted = false;
        player.transform.position = playerStartPosition;
        createJelly();
    }

    private void createJelly()
    {
        GameObject jellyClone = Instantiate(jelly, jellyStartPosition, Quaternion.identity);
        int jellyBoneCount = jellyClone.transform.childCount;
        for (int i = 0; i < jellyBoneCount; i++)
        {
            Rigidbody2D jellyBoneBody = jellyClone.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
            jellyBoneBody.Sleep();
        }
    }
}
