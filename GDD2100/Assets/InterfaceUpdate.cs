using UnityEngine;

public class InterfaceUpdate : MonoBehaviour
{
    static InterfaceUpdate _instance;

    [SerializeField] TMPro.TMP_Text[] UIText;

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
                    textElement.text = "Score: " + PointManager.Instance.Points.ToString();
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
                    Debug.Log("Accuracy: " + (((float)pointManager.Points / (float)pointManager.BallsFired) * 100) + "%");
                    Debug.Log((float)pointManager.Points / (float)pointManager.BallsFired);
                    Debug.Log("Points: " + pointManager.Points);
                    Debug.Log("Balls Fired: " + pointManager.BallsFired);
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
                    break;
                case "LevelText":
                    textElement.text = "Level: " + pointManager.Level;
                    break;
                default:
                    Debug.LogWarning("Unknown UI element: " + textElement.name);
                    break;
            }
        }

        Debug.Log("User interface updated.");
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
