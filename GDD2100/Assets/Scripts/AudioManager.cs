using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI closedCaptionsText;
    public AudioClip[] audioClips;

    private bool closedCaptionsEnabled = true;

    public void ClosedCaptionsEnabled(bool enabled)
    {
        closedCaptionsEnabled = enabled;

        if (!closedCaptionsEnabled)
        {
            closedCaptionsText.gameObject.SetActive(false);
            closedCaptionsText.gameObject.GetComponentInParent<Image>().enabled = false;
        }
    }

    private float closedCaptionPadding = 2f;

    private AudioSource audioSource;

    public void PlayAudio(AudioClip clip)
    {
        StopAllCoroutines();
        //audioSource.PlayOneShot(clip);

        audioSource.clip = clip;
        audioSource.Play();

        if (closedCaptionsEnabled)
        {
            StartCoroutine(ShowClosedCaption(clip.name, clip.length));
        }
    }

    private IEnumerator ShowClosedCaption(string caption, float duration)
    {
        closedCaptionsText.text = "* " + caption + " *";
        closedCaptionsText.gameObject.SetActive(true);
        closedCaptionsText.gameObject.GetComponentInParent<Image>().enabled = true;
        yield return new WaitForSeconds(duration + closedCaptionPadding);
        closedCaptionsText.gameObject.SetActive(false);
        closedCaptionsText.gameObject.GetComponentInParent<Image>().enabled = false;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
