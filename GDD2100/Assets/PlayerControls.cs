using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1.0f;
    public float TurnSpeed { get { return turnSpeed; } }
    Vector2 turnDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TurnCannon();    
    }

    void OnFire()
    {
        Debug.Log("Mouse Clicked!");

        FindFirstObjectByType<FireBall>().Fire();
    }

    void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        turnDirection = value.Get<Vector2>();
    }

    void OnReset()
    {
        FindFirstObjectByType<FireBall>().ResetRotation();
    }

    void OnAdjustSensitivity(UnityEngine.InputSystem.InputValue value)
    {
        if (turnSpeed > 1)
        {
            turnSpeed += value.Get<float>();
        }
        
        InterfaceUpdate.Instance.RefreshUI();
    }

    private void TurnCannon()
    {
        float x = turnDirection.x * turnSpeed * Time.deltaTime * 10;
        float y = turnDirection.y * turnSpeed * Time.deltaTime * 10;

        FireBall cannon = FindFirstObjectByType<FireBall>();



        cannon.Turn(0, y, x);
    }
}
