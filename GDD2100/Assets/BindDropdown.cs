using UnityEngine;

public class BindDropdown : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TMPro.TMP_Dropdown>().onValueChanged.AddListener(delegate
        {
            FindFirstObjectByType<DifficultySelector>().SetDifficulty(GetComponent<TMPro.TMP_Dropdown>().value);
        });
    }
}
