using UnityEngine;
using UnityEngine.InputSystem;

public class HideMouse : MonoBehaviour
{
    public PauseManager pauseManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        pauseManager = FindFirstObjectByType<PauseManager>();
        pauseManager.OnPauseToggled.AddListener(HandlePauseToggle);
    }

    private void OnDisable()
    {
        pauseManager.OnPauseToggled.RemoveListener(HandlePauseToggle);
    }

    private void HandlePauseToggle(bool isPaused)
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
