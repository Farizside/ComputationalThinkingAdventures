using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class EssayManager : MonoBehaviour
{
    [SerializeField] private List<TMP_InputField> _answers;
    [SerializeField] private List<string> _keyAnswers;

    [SerializeField] private GameObject _benarPanel;
    [SerializeField] private GameObject _salahPanel;
    [SerializeField] private GameObject _kosongPanel;
    [SerializeField] private GameObject _belumLengkapPanel;

    public bool _isOrdered = false;
    
    public int nullCount;

    private void Start()
    {
        nullCount = _keyAnswers.Count;
    }
    
    public void OnSubmit()
    {
        foreach (var answer in _answers)
        {
            if (answer.text == "")
            {
                nullCount--;
            }
        }
        
        var rightCount = 0;
        for (int i = 0; i < _keyAnswers.Count; i++)
        {
            if (!_isOrdered)
            {
                if (_keyAnswers.Contains(_answers[i].text.ToLower()))
                {
                    rightCount++;
                    _keyAnswers.IndexOf(_answers[i].text.ToLower());
                }
            }
            else
            {
                if (_answers[i].text.ToLower() == _keyAnswers[i].ToLower())
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
        
        if (nullCount > 0 && nullCount != _keyAnswers.Count)
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
