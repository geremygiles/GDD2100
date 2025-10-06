using UnityEngine;
using UnityEngine.SceneManagement;

public class PointManager : MonoBehaviour
{
    static PointManager _instance;

    public int BallsFired { get; private set; } = 0;

    public int Points { get; private set; } = 0;

    public int Level { get; private set; } = 0;

    public int Rings { get; private set; } = 5;

    public int NumOfLevels { get; private set; } = 10;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void AddPoints(int points)
    {
        Points += points;
        InterfaceUpdate.Instance.RefreshUI();
    }

    public void IncrementBalls()
    {
        BallsFired++;
    }

    public void IncrementLevel()
    {
        Level++;
        InterfaceUpdate.Instance.RefreshUI();

        if (Level > NumOfLevels)
        {
            FindFirstObjectByType<SceneManagerSingleton>().LoadEndScreen();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            Reset();
        }
        else { 
            FindAnyObjectByType<RingManager>()?.ResetRings();
        }
    }

    private void Reset()
    {
        BallsFired = 0;
        Points = 0;
        Level = 0;
        InterfaceUpdate.Instance.RefreshUI();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this);
    }

    static public PointManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<PointManager>();
            }

            return _instance;
        }
    }
}
