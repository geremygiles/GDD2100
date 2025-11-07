using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    [SerializeField] GameObject cannon;
    [SerializeField] FireDirectionPoint fireDirectionPoint;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(fireDirectionPoint.transform);
    }
}
