using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private GameObject _OnOffVolumeButton;
    [SerializeField] private Sprite _volumeOnSprite;
    [SerializeField] private Sprite _volumeOffSprite;

    [SerializeField] private AudioMixerGroup _masterVolume;
    private float _masterVolumeValue;



    public void OnMenu()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void OffMenu()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_settingsPanel.activeInHierarchy)
            {
                _settingsPanel.SetActive(false);
            }
            else if (_pausePanel.activeInHierarchy)
            {
                OffMenu();
            }
            else
            {
                OnMenu();
            }
        }
    }

    private void Start()
    {
        if (gameObject.activeInHierarchy)
        {
            OffMenu();
        }

        float value;
        _masterVolume.audioMixer.GetFloat("MasterVolume", out value);
        if (value != -80)
        {
            _masterVolumeValue = value;
            _OnOffVolumeButton.GetComponent<Image>().sprite = _volumeOnSprite;
        }
        else
        {
            _OnOffVolumeButton.GetComponent<Image>().sprite = _volumeOffSprite;
        }

        gameObject.GetComponent<SettingsMenu>().UpdateCanvas();
        if (_settingsPanel.activeInHierarchy)
        {
            _settingsPanel.SetActive(false);
        }

    }

    public void ExitPauseMenu()
    {
        _pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenSettingsMenu()
    {
        _pausePanel.SetActive(false);
        _settingsPanel.SetActive(true);
        gameObject.GetComponent<SettingsMenu>().UpdateCanvas();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeVolume()
    {
        float value;
        _masterVolume.audioMixer.GetFloat("MasterVolume", out value);
        if (value != -80)
        {
            _masterVolumeValue = value;
            _masterVolume.audioMixer.SetFloat("MasterVolume", -80);
            _OnOffVolumeButton.GetComponent<Image>().sprite = _volumeOffSprite;
            
        }
        else
        {
            _masterVolume.audioMixer.SetFloat("MasterVolume", _masterVolumeValue);
            _OnOffVolumeButton.GetComponent<Image>().sprite = _volumeOnSprite;
        }
    }

    public void ExitSettingsMenu()
    {
        _settingsPanel.SetActive(false);
        _pausePanel.SetActive(true);
    }

}
