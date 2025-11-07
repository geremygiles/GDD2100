using UnityEngine;

public class FireDirectionPoint : MonoBehaviour
{

    Vector3 position = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        ClampPosition();
    }

    public void Move(Vector3 delta)
    {
        transform.position += delta;
    }

    private void ClampPosition()
    {
        position = transform.position;
        position.x = Mathf.Clamp(position.x, -200f, 200f);
        position.y = Mathf.Clamp(position.y, 0f, 150f);
        position.z = 0f;
        transform.position = position;
    }
}
