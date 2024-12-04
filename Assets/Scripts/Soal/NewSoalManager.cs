using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NewSoalManager : MonoBehaviour
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
    
    public HashSet<string> _uniqueIPs = new HashSet<string>();

    public int nullCount;

    private void Start()
    {
        nullCount = _isEssay? _keyAnswers.Count + _keyEssayAnswers.Count : _keyAnswers.Count;
        _benarPanel.GetComponentInChildren<Button>().onClick.AddListener(() => AudioManager.Instance.PlaySFX("Button"));
    }

    public void OnSubmit()
    {
        AudioManager.Instance.PlaySFX("Button");
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
                if (CheckIP(_essayAnswers[i].text, _keyEssayAnswers[i]))
                {
                    Debug.Log(rightCount);
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

        if (_isEssay)
        {
            if (nullCount > 0 && nullCount != _keyAnswers.Count + _keyEssayAnswers.Count)
            {
                _belumLengkapPanel.SetActive(true);
                AudioManager.Instance.PlaySFX("Wrong");
                return;
            }
        }
        else
        {
            if (nullCount > 0 && nullCount != _keyAnswers.Count)
            {
                _belumLengkapPanel.SetActive(true);
                AudioManager.Instance.PlaySFX("Wrong");
                return;
            }
        }

        if (_isEssay)
        {
            if (rightCount < _keyAnswers.Count + _keyEssayAnswers.Count)
            {
                _salahPanel.SetActive(true);
                Debug.Log(rightCount);
                AudioManager.Instance.PlaySFX("Wrong");
            }
            else
            {
                _benarPanel.SetActive(true);
                AudioManager.Instance.PlaySFX("Correct");
            }
        }
        else
        {
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
    }

    public bool CheckIP(string answer, string key)
    {
        var parts = answer.Split(".");
        if (parts.Length != 4) return false;
        var first = parts[0] + "." + parts[1] + "." + parts[2];
        Debug.Log(first);
        if (first != key) return false;

        if (!int.TryParse(parts[3], out int last) || last < 2 || last > 254) return false;
        
        if (!_uniqueIPs.Add(answer))
        {
            Debug.Log($"IP {answer} sudah digunakan sebelumnya.");
            return false;
        }

        return true;
    }
    
    public void OnSkip()
    {
        var list = new List<Draggable>(FindObjectsByType<Draggable>(FindObjectsSortMode.None)); // Konversi ke List agar bisa dihapus
        var i = 0;

        foreach (var slot in _slots)
        {
            if (slot.transform.childCount == 0)
            {
                // Cari gameObject di list yang namanya sesuai dengan _keyAnswers[i]
                var answer = list.FirstOrDefault(draggable => draggable.gameObject.name == _keyAnswers[i]);

                if (answer != null)
                {
                    if (answer.isDuplicatedOnDrag)
                    {
                        var duplicateObject = Instantiate(answer, slot.transform);
                        duplicateObject.name = answer.name;
                    }
                    else
                    {
                        // Atur parent gameObject tersebut ke slot
                        answer.transform.SetParent(slot.gameObject.transform);

                        // Hapus dari list
                        list.Remove(answer);
                    }
                }
            }

            i++;
        }

        if (_isEssay)
        {
            var e = 0;
            foreach (var answer in _essayAnswers)
            {
                answer.text = _keyEssayAnswers[e] + "." + Random.Range(2,254);
                e++;
            }
        }
    }
}
