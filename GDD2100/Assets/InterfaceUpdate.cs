using UnityEngine;

public class InterfaceUpdate : MonoBehaviour
{
    static InterfaceUpdate _instance;

    [SerializeField] TMPro.TMP_Text[] UIText;

    public void RefreshUI()
    {
        foreach (var textElement in UIText)
        {
            switch (textElement.name)
            {
                case "ScoreText":
                    textElement.text = "Score: " + PointManager.Instance.Points.ToString();
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
