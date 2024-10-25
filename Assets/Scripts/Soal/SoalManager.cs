using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoalManager : MonoBehaviour
{
    [SerializeField] private List<Dropable> _slots;
    [SerializeField] private List<string> _keyAnswers;
    [SerializeField] private List<string> _answers;

    [SerializeField] private GameObject _benarPanel;
    [SerializeField] private GameObject _salahPanel;
    [SerializeField] private GameObject _kosongPanel;
    [SerializeField] private GameObject _belumLengkapPanel;

    public int nullCount;
    public void OnSubmit()
    {
        CheckAnswers();
    }

    private void CheckAnswers()
    {
        _answers.Clear();

        nullCount = _keyAnswers.Count;
        foreach (var _slot in _slots)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answers.Add(answer.name);
            }
            catch (Exception e)
            {
                _answers.Add("null");
                nullCount--;
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

        if (nullCount == 0)
        {
            _kosongPanel.SetActive(true);
            return;
        }

        if (nullCount > 0 && nullCount < _keyAnswers.Count)
        {
            _belumLengkapPanel.SetActive(true);
            return;
        }
        
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
