using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCollectorController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _healthBarImage;
    [SerializeField] private ParticleSystem _completeParticleSystem;
    [SerializeField] private ParticleSystem _lessingParticleSystem;
    private bool OnPause = false;

    private void Start()
    {
        _slider.value = _slider.maxValue;
        StartCoroutine(LessingValue());
    }

    public void SetPauseBool(bool isPause)
    {
        OnPause = isPause;
    }


    private IEnumerator LessingValue()
    {
        while (_slider.value != _slider.minValue)
        {
            yield return null;
            if (OnPause == false)
            {
                _slider.value -= 1f;
            }
        }
        StartCoroutine(RestartLvl());
    }

    [SerializeField] private Image _backgroundImage;

    private IEnumerator RestartLvl()
    {
        for (int i = 0; i < 2; i++)
        {
            _backgroundImage.color = Color.black;
            yield return new WaitForSeconds(0.25f);
            _backgroundImage.color = Color.red;
            yield return new WaitForSeconds(0.25f);
        }
        _backgroundImage.color = Color.black;

        GameObject.FindObjectOfType<RestartLevel>().StartRestart();
    }

    public void Complete()
    {
        StopAllCoroutines();
        StartCoroutine(Completing());
    }

    private IEnumerator Completing()
    {
        _lessingParticleSystem.Stop();

        float step = (_slider.maxValue - _slider.value) / 3;

        while (_slider.value < _slider.maxValue)
        {
            _slider.value += step;
            _completeParticleSystem.Play();
            yield return new WaitForSeconds(0.5f);
        }

        _slider.gameObject.SetActive(false);
        _healthBarImage.SetActive(false);
    }




}
