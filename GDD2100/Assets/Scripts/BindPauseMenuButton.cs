using UnityEngine;
using UnityEngine.UI;

public class BindPauseMenuButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponent<TMPro.TMP_Dropdown>() != null)
        {
            GetComponent<TMPro.TMP_Dropdown>().onValueChanged.AddListener(delegate
            {
                FindFirstObjectByType<DifficultySelector>().SetDifficulty(GetComponent<TMPro.TMP_Dropdown>().value);
            });
        }
            
        if (GetComponent<Toggle>() != null)
        {
            GetComponent<Toggle>().onValueChanged.AddListener(delegate
            {
                FindFirstObjectByType<AudioManager>().ClosedCaptionsEnabled(GetComponent<Toggle>().isOn);
            });
        }

            

        if (tag == "PauseMenuButton")
        {
            GetComponent<Button>().onClick.AddListener(delegate
            {
                FindFirstObjectByType<PauseManager>().TogglePause();
            });
        }
        else if (tag == "MainMenuButton")
        {
            GetComponent<Button>().onClick.AddListener(delegate
            {
                SceneManagerSingleton.Instance.LoadMenu();
            });
        }
    }
}
