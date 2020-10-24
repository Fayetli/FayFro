using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{

    [SerializeField] private Texture2D _fadeOutTexture;
    [SerializeField] private float _fadeSpeed = 0.8f;

    private int _drawDepth = -1000;
    private float _alpha = 1.0f;
    private int _fadeDir = -1;


    private void OnGUI()
    {
        _alpha += _fadeDir * _fadeSpeed * Time.deltaTime;
        _alpha = Mathf.Clamp01(_alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, _alpha);
        GUI.depth = _drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _fadeOutTexture);

    }

    public float BeginFade(int direction)
    {
        _fadeDir = direction;
        return _fadeSpeed;
    }
    
    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }

  

}
