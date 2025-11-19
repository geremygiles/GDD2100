using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Image focusPointPrefab;
    [SerializeField] GameObject darkBackground;
    [SerializeField] TMPro.TextMeshProUGUI tutorialTextPrefab;

    [SerializeField] TutorialState[] tutorialStates;
    private int currentStateIndex = 0;
    public bool tutorialActive = true;

    List<GameObject> currentUIElements = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadState();
    }

    private GameObject AddFocusPoint(float x, float y, float radius)
    {
        Image focusPoint = Instantiate(focusPointPrefab, transform);
        focusPoint.transform.localPosition = new Vector3(x, y, 0);
        focusPoint.rectTransform.sizeDelta = new Vector2(radius, radius);
        darkBackground.transform.SetAsFirstSibling();
        focusPoint.transform.SetAsFirstSibling();
        
        return focusPoint.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadState()
    {
        TutorialState currentState = tutorialStates[currentStateIndex];

        if (currentState.loadMode == StateLoadMode.Replace)
        {
            ClearUI();
        }

        if (currentState.text == "")
        {
            Debug.LogWarning("Tutorial State has no text to display.");
        } 
        if (currentState.text != "")
        {
            // Create and position text UI element
            GameObject textElementObject = Instantiate(tutorialTextPrefab.gameObject, transform);
            textElementObject.GetComponent<TMPro.TextMeshProUGUI>().text = currentState.text;
            textElementObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentState.textXPosition, currentState.textYPosition);
            currentUIElements.Add(textElementObject);
        }

        if (currentState.hasFocusPoint)
        {
            // Create and position focus point UI element
            GameObject focusPointObject = AddFocusPoint(currentState.focusPointPosition.x, currentState.focusPointPosition.y, currentState.focusPointRadius);
            currentUIElements.Add(focusPointObject);
        }
    }

    private void ClearUI()
    {
        if (currentUIElements.Count == 0) return;
        foreach (GameObject uiElement in currentUIElements)
        {
            Destroy(uiElement);
        }
    }
    
    public void AdvanceTutorial()
    {
        Debug.Log("Advancing tutorial...");
        if (!tutorialActive) return;
        currentStateIndex++;
        if (currentStateIndex >= tutorialStates.Length)
        {
            tutorialActive = false;
            ClearUI();
            return;
        }
        LoadState();
    }

    private void EndTutorial()
    {
        tutorialActive = false;
        ClearUI();
        Destroy(transform.parent.gameObject);
    }
}
