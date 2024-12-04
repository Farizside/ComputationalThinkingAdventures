using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        _benarPanel.GetComponentInChildren<Button>().onClick.AddListener(() => AudioManager.Instance.PlaySFX("Button"));
    }
    
    public void OnSubmit()
    {
        AudioManager.Instance.PlaySFX("Button");
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
            AudioManager.Instance.PlaySFX("Wrong");
            return;
        }
        
        if (nullCount > 0 && nullCount != _keyAnswers.Count)
        {
            _belumLengkapPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Wrong");
            return;
        }
        
        if (rightCount < _keyAnswers.Count)
        {
            _salahPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Wrong");
        }
        else
        {
            _benarPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Correct");
        }
    }

    public void OnSkip()
    {
        var e = 0;
        foreach (var answer in _answers)
        {
            answer.text = _keyAnswers[e];
            e++;
        }
    }
}
