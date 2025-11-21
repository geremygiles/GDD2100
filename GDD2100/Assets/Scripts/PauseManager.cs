using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPrefab;

    public GameObject PauseMenu { get; private set; }

    public bool IsPaused { get; private set; } = false;

    public UnityEvent<bool, bool> OnPauseToggled;

    private void Start()
    {
        LoadPauseMenu();
    }

    private void LoadPauseMenu(Scene scene, LoadSceneMode mode)
    {
        LoadPauseMenu();
    }

    private void LoadPauseMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            return; // No pause menu in main menu or instructions scene
        }

        try
        {
            PauseMenu = GameObject.FindWithTag("PauseMenu");
            Debug.Log("PauseMenu found in scene:" + PauseMenu.name);
        }
        catch
        {
            IsPaused = false;
            Time.timeScale = IsPaused ? 0f : 1f;

            PauseMenu = Instantiate(pauseMenuPrefab);
            PauseMenu.SetActive(false);
        }

        
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

        if (visible)
        {
            Debug.Log(PauseMenu == null ? "PauseMenu is null" : "PauseMenu is not null");
            if (PauseMenu == null)
            {
                LoadPauseMenu();
            }
            PauseMenu.SetActive(IsPaused);
        }

        OnPauseToggled?.Invoke(IsPaused, visible); // Toggles mouse in Hide Mouse and movement in PlayerControls
    }

    private void OnDisable()
    {
        
    }
}
