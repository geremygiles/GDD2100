using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1.0f;
    bool turning = false;
    Vector2 turnDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (turning)
        {
            TurnCannon();
        }
        
    }

    void OnFire()
    {
        Debug.Log("Mouse Clicked!");

        FindFirstObjectByType<FireBall>().Fire();
    }

    void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        turning = !turning;
        turnDirection = value.Get<Vector2>();
    }

    private void TurnCannon()
    {
        float x = turnDirection.x * turnSpeed;
        float y = turnDirection.y * turnSpeed;

        FireBall cannon = FindFirstObjectByType<FireBall>();



        cannon.Turn(0, y, x);
    }
}
