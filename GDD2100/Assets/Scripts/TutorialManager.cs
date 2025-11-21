using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Image focusPointPrefab;
    [SerializeField] GameObject darkBackground;
    [SerializeField] TMPro.TextMeshProUGUI tutorialTextPrefab;
    [SerializeField] TMPro.TextMeshProUGUI clickText;

    TutorialState[] tutorialStates;
    private int currentStateIndex = 0;
    public bool tutorialActive = true;

    private PlayerControls playerControls;

    List<GameObject> currentUIElements = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadAllStates();

        LoadState();

        try
        {
            playerControls = FindFirstObjectByType<PlayerControls>();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("No PlayerController found in scene to disable movement.");
            return;
        }
    }

    private void LoadAllStates()
    {
        tutorialStates = Resources.LoadAll<TutorialState>("Tutorial States");
        Debug.Log("Loaded " + tutorialStates.Length + " tutorial states.");
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

        // Wait if there's a delay
        //if (currentState.delay > 0)
       //{
        //    Debug.Log("Delaying tutorial state for " + currentState.delay + " seconds.");
        //    Invoke(nameof(LoadState), currentState.delay);
        //    return;
        //}

        // Clear previous listeners to avoid multiple triggers
        FindFirstObjectByType<PointManager>().OnScorePoint.RemoveListener(AdvanceTutorial);
        FindFirstObjectByType<CameraController>().OnZoomToggled.RemoveListener(AdvanceTutorial);
        FindFirstObjectByType<PlayerControls>().ClickDetected.RemoveListener(AdvanceTutorial);
        FindFirstObjectByType<PlayerControls>().canFire = true;
        clickText.gameObject.SetActive(false);

        // Set up listeners based on advance action
        switch (currentState.advanceAction)
        {
            case AdvanceAction.Click:
                // Ensure cannon firing is disabled
                FindFirstObjectByType<PlayerControls>().canFire = false;
                FindFirstObjectByType<PlayerControls>().ClickDetected.AddListener(AdvanceTutorial);
                clickText.gameObject.SetActive(true);
                break;
            case AdvanceAction.Score:
                // Advance when player scores a point
                FindFirstObjectByType<PointManager>().OnScorePoint.AddListener(AdvanceTutorial);
                break;
            case AdvanceAction.Zoom:
                // Wait for a zoom to advance
                FindFirstObjectByType<CameraController>().OnZoomToggled.AddListener(AdvanceTutorial);
                break;
            default:
                Debug.LogWarning("Unknown advance action.");
                break;
        }

        // Disable or enable player movement based on state
        FindFirstObjectByType<PauseManager>().SetPause(!currentState.allowMovement, false);

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
            EndTutorial();
            return;
        }
        LoadState();
    }

    private void EndTutorial()
    {


        tutorialActive = false;
        ClearUI();
        SceneManager.UnloadSceneAsync("Tutorial");
    }

    private void OnDestroy()
    {
        // Clean up listeners
        FindFirstObjectByType<PointManager>()?.OnScorePoint.RemoveListener(AdvanceTutorial);
        FindFirstObjectByType<CameraController>()?.OnZoomToggled.RemoveListener(AdvanceTutorial);
        FindFirstObjectByType<PlayerControls>()?.ClickDetected.RemoveListener(AdvanceTutorial);
        FindFirstObjectByType<PlayerControls>().canFire = true;
        FindFirstObjectByType<PauseManager>().SetPause(false, false);
    }
}
