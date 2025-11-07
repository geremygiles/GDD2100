using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineCamera standardCamera;
    [SerializeField] CinemachineCamera zoomedCamera;
    public void ToggleZoom()
    {
        standardCamera.enabled = !standardCamera.enabled;
    }
}
