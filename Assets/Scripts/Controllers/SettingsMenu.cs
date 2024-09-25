using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {

    private Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resolutionDropDown;

    private void Awake() {
        // Get available screen resolutions
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        // Populate the dropdown with available resolutions
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Check if this resolution is the current screen resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i; // Save the index of the current resolution
            }
        }

        // Add options to the dropdown
        resolutionDropDown.AddOptions(options);

        // Set the dropdown to the current resolution
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        // Add listener to handle when a new resolution is selected
        resolutionDropDown.onValueChanged.AddListener(delegate {
            SetResolution(resolutionDropDown.value);
        });
    }

    // This method sets the screen resolution based on the selected index from the dropdown
    public void SetResolution(int resolutionIndex) {
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
