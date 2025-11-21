using Unity.VisualScripting;
using UnityEngine;

public enum StateLoadMode
{
    Replace = 0,
    Add = 1
}

public enum AdvanceAction
{
    Click = 0,
    Score = 1,
    Zoom = 2
}

public enum FocusTarget
{
    None = 0,
    Set = 1,
    Cannon = 2,
    Target = 3,
    UIElement = 4
}

[CreateAssetMenu(fileName = "TutorialState", menuName = "Scriptable Objects/TutorialState")]
public class TutorialState : ScriptableObject
{
    public StateLoadMode loadMode;
    public float delay = 0f;
    public AdvanceAction advanceAction;
    public bool allowMovement = false;
    [Header ("Text Settings")]
    public string text;
    public float textXPosition;
    public float textYPosition;
    [Header ("Focus Point Settings")]
    public FocusTarget focusTarget = FocusTarget.None;
    public Vector2 focusPointPosition;
    public float focusPointRadius = 100f;
}
