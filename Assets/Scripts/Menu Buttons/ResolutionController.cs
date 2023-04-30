using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    private const string SELECTED_RESOLUTION_KEY = "SelectedResolution";

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        PopulateResolutionDropdown();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Find the current resolution in the list of resolutions
        Resolution currentResolution = Screen.currentResolution;
        int currentResolutionIndex = System.Array.FindIndex(Screen.resolutions, r => r.width == currentResolution.width && r.height == currentResolution.height);

        // Check if a selected resolution is stored in PlayerPrefs
        if (PlayerPrefs.HasKey(SELECTED_RESOLUTION_KEY))
        {
            // Set the selected resolution from PlayerPrefs
            resolutionDropdown.value = PlayerPrefs.GetInt(SELECTED_RESOLUTION_KEY);
        }
        else
        {
            // Set the current resolution as the selected option in the dropdown
            resolutionDropdown.value = currentResolutionIndex;
        }
    }

    private void PopulateResolutionDropdown()
    {
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }

    }

    private void SetResolution(int resolutionIndex)
    {
        // Store the selected resolution in PlayerPrefs
        PlayerPrefs.SetInt(SELECTED_RESOLUTION_KEY, resolutionIndex);
        PlayerPrefs.Save();

        Resolution selectedResolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
