using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1.0f;
    public float TurnSpeed { get { return turnSpeed; } }
    Vector2 turnDirection = Vector2.zero;
    FireDirectionPoint fdp;
    GameObject gameManager;
    public bool canMove = true;
    public bool canFire = true;
    public bool canPause = true;
    public PauseManager pauseManager;
    public UnityEvent ClickDetected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fdp = FindFirstObjectByType<FireDirectionPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnCannon();    
    }

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        pauseManager = gameManager.GetComponent<PauseManager>();
        pauseManager.OnPauseToggled.AddListener(HandlePauseToggle);
    }

    private void OnDisable()
    {
        pauseManager.OnPauseToggled.RemoveListener(HandlePauseToggle);
    }

    private void HandlePauseToggle(bool isPaused, bool visible)
    {
        canMove = !isPaused;
    }

    void OnFire()
    {
        if (!canMove || !canFire) return;
        FindFirstObjectByType<FireBall>().Fire();
    }

    void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        if (!canMove) return;
        turnDirection = value.Get<Vector2>();
    }

    void OnReset()
    {
        FindFirstObjectByType<FireBall>().ResetRotation();
    }

    void OnQuit()
    {
        gameManager.GetComponent<HideMouse>().UnlockCursor();
        SceneManagerSingleton.Instance.LoadMenu();
    }

    void OnPause()
    {
        if (!canPause) return;
        gameManager.GetComponent<PauseManager>().TogglePause(true);
    }

    void OnAdjustSensitivity(UnityEngine.InputSystem.InputValue value)
    {
        if (!canMove) return;
        if (turnSpeed > 1 || value.Get<float>() > 0)
        {
            turnSpeed += value.Get<float>();
        }
        
        InterfaceUpdate.Instance.RefreshUI();
    }

    void OnZoomToggle()
    {
        if (!canMove) return;
        FindFirstObjectByType<CameraController>().ToggleZoom();
    }

    void OnClick()
    {
        Debug.Log("Click detected");
        ClickDetected?.Invoke();
    }

    private void TurnCannon()
    {
        if (!canMove) return;
        float x = turnDirection.x * turnSpeed / 25;
        float y = turnDirection.y * turnSpeed / 25;

        Vector3 movementDelta = new Vector3(x, y, 0);

        fdp.Move(movementDelta);
    }
}
