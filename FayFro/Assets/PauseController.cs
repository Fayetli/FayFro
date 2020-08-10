using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private bool _menuOn = false;

    public void OnMenu()
    {
        _menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void OffMenu()
    {
        _menu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Start()
    {
        if (gameObject.activeInHierarchy)
        {
            OffMenu();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menuOn)
            {
                OffMenu();
                _menuOn = false;
            }
            else
            {
                OnMenu();
                _menuOn = true;
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSettings()
    {

    }
}
