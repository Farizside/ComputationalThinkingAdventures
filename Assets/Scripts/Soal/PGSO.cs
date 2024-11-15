using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Soal PG")]
public class PGSO : ScriptableObject
{
    public Soal[] _Soals;
    
    [Serializable]
    public class Soal
    {
        public string text;
        public Jawaban[] jawaban;
    }

    [Serializable]
    public class Jawaban
    {
        public string text;
        public bool isTrue;
    }
}
