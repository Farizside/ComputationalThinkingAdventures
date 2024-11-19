using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Assets")]
public class AudioSO : ScriptableObject
{
    public BGM[] bgm;
    public SFX[] sfx;
    
    [Serializable]
    public struct BGM
    {
        public string name;
        public AudioClip audio;
    }
    
    [Serializable]
    public struct SFX
    {
        public string name;
        public AudioClip audio;
    }
}
