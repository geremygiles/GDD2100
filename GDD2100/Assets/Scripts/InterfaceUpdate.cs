using UnityEngine;

public class InterfaceUpdate : MonoBehaviour
{
    static InterfaceUpdate _instance;

    [SerializeField] TMPro.TMP_Text[] UIText;
    [SerializeField] LineRenderer shootingLine;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        PointManager pointManager = PointManager.Instance;
        foreach (var textElement in UIText)
        {
            switch (textElement.name)
            {
                case "ScoreText":
                    textElement.text = "Rings Remaining: " + ((PointManager.Instance.NumOfLevels * PointManager.Instance.Rings) - PointManager.Instance.Points);
                    break;
                case "TurnSpeedText":
                    PlayerControls playerControls = FindFirstObjectByType<PlayerControls>();
                    textElement.text = "Turn Sensitivity: " + playerControls.TurnSpeed.ToString();
                    break;
                case "AccuracyText":
                    if (pointManager.BallsFired == 0)
                    {
                        textElement.text = "Accuracy: N/A";
                        break;
                    }
                    textElement.text = "Accuracy: " + (int)(((float)pointManager.Points/(float)pointManager.BallsFired)* 100) + "%";
                    break;
                case "GradeText":
                    if (pointManager.BallsFired == 0)
                    {
                        textElement.text = "Grade: N/A";
                        break;
                    }
                    int accuracy = (int)(((float)pointManager.Points / (float)pointManager.BallsFired) * 100);
                    string grade = "F";
                    if (accuracy >= 90) grade = "A";
                    else if (accuracy >= 80) grade = "B";
                    else if (accuracy >= 70) grade = "C";
                    else if (accuracy >= 60) grade = "D";
                    textElement.text = "Grade: " + grade;


                    switch (FindAnyObjectByType<DifficultySelector>().CurrentDifficulty)
                    {
                        case DifficultySelector.DifficultyLevel.Easy:
                            ShowShootingLine(true);
                            break;
                        case DifficultySelector.DifficultyLevel.Normal:
                            ShowShootingLine(accuracy < 70);
                            break;
                        case DifficultySelector.DifficultyLevel.Hard:
                            ShowShootingLine(false);
                            break;
                        case DifficultySelector.DifficultyLevel.Impossible:
                            ShowShootingLine(false);
                            break;
                        default:
                            ShowShootingLine(false);
                            break;
                    }
                    break;
                case "LevelText":
                    textElement.text = "Level: " + (pointManager.Level + 1);
                    break;
                case "BallsText":
                    textElement.text = "Balls Fired: " + pointManager.BallsFired;
                    break;
                case "DifficultyLevelText":
                    textElement.text = "Current Difficulty: " + FindAnyObjectByType<DifficultySelector>().CurrentDifficulty.ToString();
                    break;
                default:
                    Debug.LogWarning("Unknown UI element: " + textElement.name);
                    break;
            }
        }
    }

    public void ShowShootingLine(bool status)
    {
        if (shootingLine == null)
        {
            return;
        }
        if (shootingLine.enabled != status)
        {
            shootingLine.enabled = status;
        }
    }

    static public InterfaceUpdate Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<InterfaceUpdate>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("InterfaceUpdate");
                    _instance = go.AddComponent<InterfaceUpdate>();
                }
            }
            return _instance;
        }
    }
}
