using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineCamera standardCamera;
    [SerializeField] CinemachineCamera zoomedCamera;
    [SerializeField] GameObject crosshair;

    public UnityEvent OnZoomToggled;
    private bool canZoom = true;

    public bool CanZoom
    {
        get { return canZoom; }
    }
    public void ToggleZoom()
    {
        if (!canZoom) return;

        standardCamera.enabled = !standardCamera.enabled;
        crosshair.SetActive(!standardCamera.enabled);
        OnZoomToggled.Invoke();

        canZoom = false;
        StartCoroutine(ReenableZoom());
    }

    private IEnumerator ReenableZoom()
    {
        yield return new WaitForSeconds(2f);
        canZoom = true;
    }
}
