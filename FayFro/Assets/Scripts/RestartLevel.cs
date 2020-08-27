using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{

    private float _touchStartTime;

    [SerializeField] private float _timeToReset = 0.4f;

    private int _scoreOnStart;



    private void Start()
    {
        _scoreOnStart = Score.get_value();
    }
    public IEnumerator Restart()
    {
        float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Score.set_value(_scoreOnStart);
    }

    public void StartRestart()
    {
        StartCoroutine(Restart());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)){
            _touchStartTime = Time.time;
        }
        if (Input.GetKey(KeyCode.BackQuote))
        {
            float deltaTime = Time.time - _touchStartTime;
            if(deltaTime > _timeToReset)
            {
                StartCoroutine(Restart());
            }
        }
    }




}
