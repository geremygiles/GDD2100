using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Image focusPointPrefab;
    [SerializeField] GameObject darkBackground;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddFocusPoint(0, 0, 200, 200);
        AddFocusPoint(-300, 150, 150, 150);
    }

    private void AddFocusPoint(int x, int y, float width, float height)
    {
        Image focusPoint = Instantiate(focusPointPrefab, transform);
        focusPoint.transform.localPosition = new Vector3(x, y, 0);
        focusPoint.rectTransform.sizeDelta = new Vector2(width, height);
        darkBackground.transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
