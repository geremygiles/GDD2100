using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerSingleton : MonoBehaviour
{
    static SceneManagerSingleton instance;

    public static SceneManagerSingleton Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindButtons();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMenu()
    {
        FindFirstObjectByType<HideMouse>()?.UnlockCursor();
        SceneManager.LoadScene(0);
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication()
    {
        FindFirstObjectByType<HideMouse>()?.UnlockCursor();
        Application.Quit();
    }

    public void LoadEndScreen()
    {
        FindFirstObjectByType<HideMouse>()?.UnlockCursor();
        SceneManager.LoadScene(3);
    }

    private void BindButtons()
    {
        foreach (Button button in FindObjectsByType<Button>(0))
        {
            Debug.Log(button.name);
            switch (button.name)
            {
                case "Start Button":
                    button.onClick.AddListener(StartGame);
                    Debug.Log("Start Button Found");
                    break;
                case "Instructions Button":
                    button.onClick.AddListener(LoadInstructions);
                    break;
                case "Menu Button":
                    button.onClick.AddListener(LoadMenu);
                    break;
                case "Quit Button":
                    button.onClick.AddListener(QuitApplication);
                    break;
                case "Return Button":
                    button.onClick.AddListener(LoadMenu);
                    break;
                default:
                    break;
            }
        }
    }
}
