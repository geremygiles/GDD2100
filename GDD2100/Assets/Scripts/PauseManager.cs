using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPrefab;

    public GameObject PauseMenu { get; private set; }

    public bool IsPaused { get; private set; } = false;

    public UnityEvent<bool> OnPauseToggled;

    private void Start()
    {
        LoadPauseMenu();
        SceneManager.sceneLoaded += LoadPauseMenu;
    }

    private void LoadPauseMenu(Scene scene, LoadSceneMode mode)
    {
        LoadPauseMenu();
    }

    private void LoadPauseMenu()
    {
        if (PauseMenu != null)
        {
            Destroy(PauseMenu);
        }

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            return; // No pause menu in main menu or instructions scene
        }

        IsPaused = false;
        Time.timeScale = IsPaused ? 0f : 1f;
        OnPauseToggled?.Invoke(IsPaused);

        PauseMenu = Instantiate(pauseMenuPrefab, FindFirstObjectByType<Canvas>().transform);
        PauseMenu.SetActive(false);
    }

    public void SetPause(bool pause, bool visible)
    {
        if (IsPaused != pause)
        {
            TogglePause(visible);
        }
    }

    public void TogglePause(bool visible)
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;

        if (visible && PauseMenu == null)
        {
            LoadPauseMenu();
            PauseMenu.SetActive(IsPaused);
        }
        

        OnPauseToggled?.Invoke(IsPaused); // Toggles mouse in Hide Mouse and movement in PlayerControls, and prompts difficulty selector to find difficulty text
    }
}
