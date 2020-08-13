using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public class BackgroundAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _backgroundClips;
    [SerializeField] private AudioSource _audio;


    private int currentTrack = 0;
    private void Awake()
    {
        _audio.clip = _backgroundClips[0];
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(_audio.isPlaying == false)
        {
            currentTrack += 1;
            if(currentTrack >= _backgroundClips.Length)
            {
                currentTrack = 0;
            }
            _audio.clip = _backgroundClips[currentTrack];
            _audio.Play();
        }
    }
}
