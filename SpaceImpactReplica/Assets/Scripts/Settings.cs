using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    Resolution[] resolutions;

    public Dropdown resolutionDropDown;
    public Dropdown graphicsDropDown;
    public GameObject settings;
    public Slider volumeSlider;

    private void Start() {
        if (PlayerPrefs.GetInt("ChangeGraphics") == 1) {
            graphicsDropDown.value = PlayerPrefs.GetInt("GraphicsIndex");
        }
        PlayerPrefs.SetInt("LoadSettings",1);
        if (PlayerPrefs.GetInt("LoadSettings") == 1){
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = volumeSlider.value;
            PlayerPrefs.SetInt("LoadSettings", 0);
            resolutions = Screen.resolutions;
            resolutionDropDown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolution = 0;
            for (int i = 0; i < resolutions.Length; i++) {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                    currentResolution = i;
                }
            }
            resolutionDropDown.AddOptions(options);
            if (Screen.fullScreen) {
                resolutionDropDown.value = currentResolution;
                resolutionDropDown.RefreshShownValue();
            } else {
                resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionIndex");
                resolutionDropDown.RefreshShownValue();
            }
        }
    }

    public void ResetScores() {
        PlayerPrefs.SetInt("Level1", 0);
        PlayerPrefs.SetInt("Level2", 0);
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetSettings(bool condition) {
        settings.SetActive(condition);
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void Quality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
        graphicsDropDown.value = qualityIndex;
        PlayerPrefs.SetInt("ChangeGraphics", 1);
        PlayerPrefs.SetInt("GraphicsIndex", graphicsDropDown.value);
    }

    public void FullScreen(bool toggleFullScreen) {
        Screen.fullScreen = toggleFullScreen;
    }
}
