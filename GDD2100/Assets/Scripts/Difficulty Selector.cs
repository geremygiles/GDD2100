using UnityEngine;
using UnityEngine.Events;

public class DifficultySelector : MonoBehaviour
{
    private TMPro.TextMeshProUGUI difficultyDescriptionText;

    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard,
        Impossible,
    }

    private DifficultyLevel difficultyLevel;

    public DifficultyLevel CurrentDifficulty
    {
        get
        {
            return (DifficultyLevel)difficultyLevel;
        }
    }

    public UnityEvent<DifficultyLevel> OnDifficultyChanged;
    public PauseManager pauseManager;

    public void SetDifficulty(int level)
    {
        // Set default difficulty level
        difficultyLevel = (DifficultyLevel)level;

        UpdateLevel();
        Debug.Log("Difficulty level set to: " + difficultyLevel.ToString());
        OnDifficultyChanged?.Invoke(difficultyLevel); // Changes range of rings in RingManager
    }

    private void UpdateLevel()
    {
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                Debug.Log("Difficulty set to Easy");
                difficultyDescriptionText.text = "Rings will appear closer to the cannon and the aim assist will always be active.";
                break;
            case DifficultyLevel.Normal:
                difficultyDescriptionText.text = "Rings will appear normally and the aim assist will activate under 70% accuracy.";
                break;
            case DifficultyLevel.Hard:
                difficultyDescriptionText.text = "Rings will appear far away from the cannon and the aim assist will never activate.";
                break;
            case DifficultyLevel.Impossible:
                difficultyDescriptionText.text = "Rings will appear very far away from the cannon and the level will reset after 20 seconds.";
                break;
            default:
                Debug.Log("Unknown difficulty level");
                break;
        }
    }

    private void OnEnable()
    {
        pauseManager = GetComponent<PauseManager>();
        pauseManager.OnPauseToggled.AddListener(HandlePauseToggle);
    }

    private void OnDisable()
    {
        pauseManager.OnPauseToggled.RemoveListener(HandlePauseToggle);
    }

    private void HandlePauseToggle(bool isPaused)
    {
        if (!isPaused) return;
        difficultyDescriptionText = GameObject.Find("Difficulty Description").GetComponent<TMPro.TextMeshProUGUI>();
    }
}
