using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCollectorController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
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
        while(_slider.value != _slider.minValue)
        {
            yield return null;
            if(OnPause == false)
            {
                _slider.value -= 1f;
            }
        }
        GameObject.FindObjectOfType<RestartLevel>().StartRestart();   
    }



}
