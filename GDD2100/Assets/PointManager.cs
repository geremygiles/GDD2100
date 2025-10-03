using UnityEngine;

public class PointManager : MonoBehaviour
{
    static PointManager _instance;

    public int Points { get; private set; } = 0;

    public void AddPoints(int points)
    {
        Points += points;
        InterfaceUpdate.Instance.RefreshUI();
    }

    static public PointManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<PointManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("PointManager");
                    _instance = go.AddComponent<PointManager>();
                }
            }
            return _instance;
        }
    }
}
