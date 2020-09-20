using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeatherCollector : MonoBehaviour
{
    [SerializeField] private int _stageID;
    private bool _isActivated = false;
    private int _sceneID;

    private void Start()
    {
        _sceneID = SceneManager.GetActiveScene().buildIndex;

        _isActivated = BonusController.CheckForActivated(_stageID, _sceneID);
        if (_isActivated)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            BonusController.SetToActivated(_stageID, _sceneID);
            Destroy(gameObject);
        }
    }
}
