using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineCamera standardCamera;
    [SerializeField] CinemachineCamera zoomedCamera;
    [SerializeField] GameObject crosshair;

    public UnityEvent OnZoomToggled;
    public void ToggleZoom()
    {
        standardCamera.enabled = !standardCamera.enabled;
        crosshair.SetActive(!standardCamera.enabled);
        OnZoomToggled.Invoke();
    }
}
