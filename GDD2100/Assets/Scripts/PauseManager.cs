using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPrefab;

    public GameObject PauseMenu { get; private set; }

    public bool IsPaused { get; private set; } = false;

    public UnityEvent<bool> OnPauseToggled;

    private void Start()
    {
        PauseMenu = Instantiate(pauseMenuPrefab, FindFirstObjectByType<Canvas>().transform);
        PauseMenu.SetActive(false);
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        PauseMenu.SetActive(IsPaused);

        OnPauseToggled?.Invoke(IsPaused); // Toggles mouse in Hide Mouse and movement in PlayerControls
    }
}
