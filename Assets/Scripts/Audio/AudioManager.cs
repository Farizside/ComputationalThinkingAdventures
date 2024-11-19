using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSO _audioAsset;
    public AudioSource _bgmSource;
    public AudioSource _sfxSource;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this);
        } 
    }

    private void Start()
    {
        PlayBGM("Home");
    }

    public void PlayBGM(string name)
    {
        foreach (var bgm in _audioAsset.bgm)
        {
            if (bgm.name == name)
            {
                _bgmSource.clip = bgm.audio;
                _bgmSource.Play();
                return;
            }
        }
    }

    public void PlaySFX(string name)
    {
        foreach (var sfx in _audioAsset.sfx)
        {
            if (sfx.name == name)
            {
                _sfxSource.PlayOneShot(sfx.audio);
                return;
            }
        }
    }

    public void Mute()
    {
        if (_bgmSource.mute)
        {
            _bgmSource.mute = false;
            _sfxSource.mute = false;
        }
        else
        {
            _bgmSource.mute = true;
            _sfxSource.mute = true;
        }
    }
}
