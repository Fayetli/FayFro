using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] private GameObject _destroyPlayerObject = null;

    public void ResetPlayer(Vector3 newPosition)
    {
        GameObject playerDstroyAnim = Instantiate(_destroyPlayerObject);
        playerDstroyAnim.transform.position = gameObject.transform.position;

        gameObject.transform.position = newPosition;
    }
    
}
