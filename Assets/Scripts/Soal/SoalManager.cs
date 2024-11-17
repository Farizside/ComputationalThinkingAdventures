using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class SoalManager : MonoBehaviour
{
    [SerializeField] private List<Dropable> _slots;
    [SerializeField] private List<string> _keyAnswers;
    [SerializeField] private List<string> _answers;

    [SerializeField] private bool _isEssay = false;
    [SerializeField] [CanBeNull] private List<TMP_InputField> _essayAnswers;
    [SerializeField] [CanBeNull] private List<string> _keyEssayAnswers;

    [SerializeField] private GameObject _benarPanel;
    [SerializeField] private GameObject _salahPanel;
    [SerializeField] private GameObject _kosongPanel;
    [SerializeField] private GameObject _belumLengkapPanel;

    public int nullCount;

    private void Start()
    {
        nullCount = _isEssay? _keyAnswers.Count + _keyEssayAnswers.Count : _keyAnswers.Count;
    }

    public void OnSubmit()
    {
        CheckAnswers();
    }

    private void CheckAnswers()
    {
        _answers.Clear();

        foreach (var _slot in _slots)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answers.Add(answer.name);
            }
            catch (Exception)
            {
                if (!_slot.isCable)
                {
                    _answers.Add("null");
                    nullCount--;
                }
                else
                {
                    _answers.Add("");
                }
            }
        }

        if (_isEssay)
        {
            foreach (var answer in _essayAnswers)
            {
                if (answer.text == "")
                {
                    nullCount--;
                }
            }
        }

        var rightCount = 0;
        for (int i = 0; i < _keyAnswers.Count; i++)
        {
            if (_answers[i] == _keyAnswers[i])
            {
                rightCount++;
            }
        }

        if (_isEssay)
        {
            for (int i = 0; i < _keyEssayAnswers.Count; i++)
            {
                if (_essayAnswers[i].text.ToLower() == _keyEssayAnswers[i].ToLower())
                {
                    rightCount++;
                }
            }
        }

        if (nullCount == 0)
        {
            _kosongPanel.SetActive(true);
            return;
        }

        if (_isEssay)
        {
            if (nullCount > 0 && nullCount != _keyAnswers.Count + _keyEssayAnswers.Count)
            {
                _belumLengkapPanel.SetActive(true);
                return;
            }
        }
        else
        {
            if (nullCount > 0 && nullCount != _keyAnswers.Count)
            {
                _belumLengkapPanel.SetActive(true);
                return;
            }
        }

        if (_isEssay)
        {
            if (rightCount < _keyAnswers.Count + _keyEssayAnswers.Count)
            {
                _salahPanel.SetActive(true);
            }
            else
            {
                _benarPanel.SetActive(true);
            }
        }
        else
        {
            if (rightCount < _keyAnswers.Count)
            {
                _salahPanel.SetActive(true);
            }
            else
            {
                _benarPanel.SetActive(true);
            }
        }
    }
}
