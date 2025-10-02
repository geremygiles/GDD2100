using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        Debug.Log("Mouse Clicked!");

        FindFirstObjectByType<FireBall>().Fire();
    }

    void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        Vector2 movementVector = value.Get<Vector2>();
        float x = movementVector.y;
        float y = movementVector.x;

        FindFirstObjectByType<FireBall>().Turn(x, y, 0);
    }
}
