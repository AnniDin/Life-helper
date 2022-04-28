using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    float volume;
    int qualityIndex;
    bool isFullscreen;
    int resolutionWidth;
    int resolutionHeight;
    float textSpeed;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i =0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionWidth = resolution.width;
        resolutionHeight = resolution.height;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        this.volume = volume;
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        this.qualityIndex = qualityIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        this.isFullscreen = isFullscreen;
    }

    public void SetTextSpeed(float textSpeed)
    {
        this.textSpeed = textSpeed;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
        PlayerPrefs.SetString("isFullscreen", isFullscreen.ToString());
        PlayerPrefs.SetInt("resolutionWidth", resolutionWidth);
        PlayerPrefs.SetInt("resolutionHeight", resolutionHeight);
        PlayerPrefs.SetFloat("textSpeed", textSpeed);
    }

    private void OnEnable()
    {
        volume = PlayerPrefs.GetFloat("volume", 0f);
        qualityIndex = PlayerPrefs.GetInt("qualityIndex", 2);

        if (PlayerPrefs.GetString("isFullscreen", "true") == "true")
        {
            isFullscreen = true;
        }
        else
        {
            isFullscreen = false;
        }
        resolutionWidth = PlayerPrefs.GetInt("resolutionWidth", 1920);
        resolutionHeight = PlayerPrefs.GetInt("resolutionHeight", 1080);
        textSpeed = PlayerPrefs.GetFloat("textSpeed", 0.01f);

        Screen.SetResolution(resolutionWidth, resolutionHeight, Screen.fullScreen);
        audioMixer.SetFloat("volume", volume);
        QualitySettings.SetQualityLevel(qualityIndex);
        Screen.fullScreen = isFullscreen;
    }
}
