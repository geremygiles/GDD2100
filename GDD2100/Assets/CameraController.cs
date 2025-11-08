using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineCamera standardCamera;
    [SerializeField] CinemachineCamera zoomedCamera;
    [SerializeField] GameObject crosshair;
    public void ToggleZoom()
    {
        standardCamera.enabled = !standardCamera.enabled;
        crosshair.SetActive(!standardCamera.enabled);
    }
}
