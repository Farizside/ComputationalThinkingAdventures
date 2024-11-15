using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PGManager : MonoBehaviour
{
    [SerializeField] private PGSO _soal;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private Button[] _answerButton;
    [SerializeField] private TMP_Text[] _answerText;
    [SerializeField] private TMP_Text _indexText;

    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private TMP_Text _scoreText;

    private int _index = 0;
    private int _correctAnswer;

    private void Start()
    {
        LoadSoal(_index);
    }

    public void LoadSoal(int idx)
    {
        if (_index >= _soal._Soals.Length)
        {
            _finishPanel.SetActive(true);
            _scoreText.text = (_correctAnswer * 10).ToString();
            return;
        }
        var curSoal = _soal._Soals[idx];

        _questionText.text = curSoal.text;
        _indexText.text = (_index + 1).ToString();

        for (int i = 0; i < curSoal.jawaban.Length; i++)
        {
            _answerButton[i].onClick.RemoveAllListeners();
            _answerText[i].text = curSoal.jawaban[i].text;
            if (curSoal.jawaban[i].isTrue)
            {
                _answerButton[i].onClick.AddListener(OnRightAnswer);
            }
            else
            {
                _answerButton[i].onClick.AddListener(OnWrongAnswer);
            }
        }
    }

    public void OnRightAnswer()
    {
        _correctAnswer++;
        _index++;
        LoadSoal(_index);
    }

    public void OnWrongAnswer()
    {
        _index++;
        LoadSoal(_index);
    }
}
