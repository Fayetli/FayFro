using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _masterVolume;
    [SerializeField] private AudioMixerGroup _musicVolume;
    [SerializeField] private AudioMixerGroup _soundVolume;


    [SerializeField] private Dropdown _resolutionDropdown;
    [SerializeField] private Toggle _fullscrenToggle;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    Resolution[] _resolutions;


    private void Start()
    {
        UpdateCanvas();
    }



    public void SetVolume(float volume)
    {
        _masterVolume.audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume.audioMixer.SetFloat("MusicVolume", volume);

    }

    public void SetSoundVolume(float volume)
    {
        _soundVolume.audioMixer.SetFloat("SoundVolume", volume);

    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("Resolution.Width", resolution.width);
        PlayerPrefs.SetInt("Resolution.Height", resolution.height);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateCanvas()
    {
        _fullscrenToggle.isOn = Screen.fullScreen;

        float value;
        _masterVolume.audioMixer.GetFloat("MasterVolume", out value);
        _masterSlider.value = value;

        _musicVolume.audioMixer.GetFloat("MusicVolume", out value);
        _musicSlider.value = value;

        _soundVolume.audioMixer.GetFloat("SoundVolume", out value);
        _soundSlider.value = value;

        /////////////////////////////////
        _resolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentWidth = PlayerPrefs.GetInt("Resolution.Width");
        int currentHeight = PlayerPrefs.GetInt("Resolution.Height");
        if (currentWidth == 0 || currentHeight == 0)
        {
            currentWidth = Screen.currentResolution.width;
            currentHeight = Screen.currentResolution.height;
        }

        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == currentWidth && _resolutions[i].height == currentHeight)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }
}
