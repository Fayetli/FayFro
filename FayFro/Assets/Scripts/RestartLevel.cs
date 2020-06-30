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
        _scoreOnStart = StatCharacterController.player.GetScore();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        int minusScore = -1 * (StatCharacterController.player.GetScore() - _scoreOnStart);
        StatCharacterController.player.AddScore(minusScore);
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
                Restart();
            }
        }
    }




}
