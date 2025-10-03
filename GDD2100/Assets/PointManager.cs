using UnityEngine;

public class PointManager : MonoBehaviour
{
    static PointManager _instance;

    public int BallsFired { get; private set; } = 0;

    public int Points { get; private set; } = 0;

    public int Level { get; private set; } = 0;

    public void AddPoints(int points)
    {
        Points += points;
        InterfaceUpdate.Instance.RefreshUI();
    }

    public void IncrementBalls()
    {
        BallsFired++;
        InterfaceUpdate.Instance.RefreshUI();
    }

    public void IncrementLevel()
    {
        Level++;
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
