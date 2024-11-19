using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewPGManager : MonoBehaviour
{
    [SerializeField] private ToggleGroup[] _answers;
    [SerializeField] private List<string> _keyAnswers;
    [SerializeField] private List<string> _finalAnswers;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _congratText;

    private int _score = 0;
    
    public void OnFinish()
    {
        foreach (var _answer in _answers)
        {
            _answer.gameObject.transform.parent.gameObject.SetActive(true);
            if (_answer.GetFirstActiveToggle())
            {
                _finalAnswers.Add(_answer.GetFirstActiveToggle().name);
            }
            else
            {
                _finalAnswers.Add("kosong");
            }
        }

        for (int i = 0; i < _keyAnswers.Count; i++)
        {
            if (_keyAnswers[i].ToLower() == _finalAnswers[i].ToLower())
            {
                _score += 10;
            }
        }
        
        _finishPanel.SetActive(true);
        _scoreText.text = _score.ToString();
        _congratText.text = _congratText.text.Replace("kamu", PlayerData.Name);
    }

    public void ButtonSFX()
    {
        AudioManager.Instance.PlaySFX("Button");
    }
}
