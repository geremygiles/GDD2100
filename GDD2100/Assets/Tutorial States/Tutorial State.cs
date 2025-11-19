using UnityEngine;

public enum StateLoadMode
{
    Replace = 0,
    Add = 1
}

[CreateAssetMenu(fileName = "TutorialState", menuName = "Scriptable Objects/TutorialState")]
public class TutorialState : ScriptableObject
{
    public StateLoadMode loadMode;
    [Header ("Text Settings")]
    public string text;
    public float textXPosition;
    public float textYPosition;
    [Header ("Focus Point Settings")]
    public bool hasFocusPoint;
    public Vector2 focusPointPosition;
    public float focusPointRadius;
}
