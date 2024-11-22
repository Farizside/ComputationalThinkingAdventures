using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortBuku : MonoBehaviour
{
    [SerializeField] private List<Dropable> _column1;
    [SerializeField] private List<Dropable> _column2;
    [SerializeField] private List<Dropable> _column3;
    
    [SerializeField] private List<string> _answersColumn1;
    [SerializeField] private List<string> _answersColumn2;
    [SerializeField] private List<string> _answersColumn3;

    [SerializeField] private GameObject _benarPanel;
    [SerializeField] private GameObject _salahPanel;
    [SerializeField] private GameObject _kosongPanel;
    [SerializeField] private GameObject _belumLengkapPanel;

    public int nullCount;
    public void OnSubmit()
    {
        AudioManager.Instance.PlaySFX("Button");
        CheckAnswers();
    }

    private void CheckAnswers()
    {
        _answersColumn1.Clear();
        _answersColumn2.Clear();
        _answersColumn3.Clear();

        nullCount = _column1.Count + _column2.Count + _column3.Count;
        
        foreach (var _slot in _column1)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answersColumn1.Add(answer.name);
            }
            catch (Exception)
            {
                _answersColumn1.Add("null");
                nullCount--;
            }
        }
        
        foreach (var _slot in _column2)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answersColumn2.Add(answer.name);
            }
            catch (Exception)
            {
                _answersColumn2.Add("null");
                nullCount--;
            }
        }
        
        foreach (var _slot in _column3)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answersColumn3.Add(answer.name);
            }
            catch (Exception)
            {
                _answersColumn3.Add("null");
                nullCount--;
            }
        }

        if (nullCount == 0)
        {
            _kosongPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Wrong");
            return;
        }

        if (nullCount > 0 && nullCount < _column1.Count + _column2.Count + _column3.Count)
        {
            _belumLengkapPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Wrong");
            return;
        }

        var rightCount = 0;

        if (_answersColumn1.All(x => x.Substring(0,x.Length-2) == _answersColumn1[0].Substring(0,x.Length-2) && x != "null"))
        {
            var lastIdx = 0;
            foreach (var answer in _answersColumn1)
            {
                var newIdx = int.Parse(answer.Substring(answer.Length-1));
                if (newIdx >= lastIdx)
                {
                    lastIdx = newIdx;
                    rightCount++;
                }
            }
        }
        if (_answersColumn2.All(x => x.Substring(0,x.Length-2) == _answersColumn2[0].Substring(0,x.Length-2) && x != "null"))
        {
            var lastIdx = 0;
            foreach (var answer in _answersColumn2)
            {
                var newIdx = int.Parse(answer.Substring(answer.Length-1));
                if (newIdx >= lastIdx)
                {
                    lastIdx = newIdx;
                    rightCount++;
                }
            }
        }
        if (_answersColumn3.All(x => x.Substring(0,x.Length-2) == _answersColumn3[0].Substring(0,x.Length-2) && x != "null"))
        {
            var lastIdx = 0;
            foreach (var answer in _answersColumn3)
            {
                var newIdx = int.Parse(answer.Substring(answer.Length-1));
                if (newIdx >= lastIdx)
                {
                    lastIdx = newIdx;
                    rightCount++;
                }
            }
        }
        
        if (rightCount < 11)
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
}
