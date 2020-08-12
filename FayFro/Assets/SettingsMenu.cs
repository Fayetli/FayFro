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
        _fullscrenToggle.isOn = Screen.fullScreen;

        SetStartResolutionToDropdown();
        







        float value;
        _masterVolume.audioMixer.GetFloat("MasterVolume", out value);
        _masterSlider.value = value;

        _musicVolume.audioMixer.GetFloat("MusicVolume", out value);
        _musicSlider.value = value;

        _soundVolume.audioMixer.GetFloat("SoundVolume", out value);
        _soundSlider.value = value;
    }

    private void SetStartResolutionToDropdown()
    {
        _resolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentWidth = PlayerPrefs.GetInt("Resolution.Width");
        int currentHeight = PlayerPrefs.GetInt("Resolution.Height");
        Debug.Log("Screen: " + Screen.currentResolution.width + " " + Screen.currentResolution.height);
        Debug.Log("Pref: " + currentWidth + " " + currentHeight);

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


    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        _masterVolume.audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        _musicVolume.audioMixer.SetFloat("MusicVolume", volume);

    }

    public void SetSoundVolume(float volume)
    {
        Debug.Log(volume);
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
        SceneManager.LoadScene(0);
    }
}
