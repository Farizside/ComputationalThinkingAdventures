using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CeritaController : MonoBehaviour
{
    [SerializeField] private float _startDelay;
    [SerializeField] private float _speed;
    [SerializeField] private string _cerita;
    [SerializeField] private TMP_Text _text;
    [SerializeField] [CanBeNull] private Button[] _buttons;

    private void Start()
    {
        _text.text = null;
        StartCoroutine(StartCerita());
    }

    IEnumerator StartCerita()
    {
        yield return new WaitForSeconds(_startDelay);
        
        foreach (var c in _cerita)
        {
            _text.text += c;
            yield return new WaitForSeconds(_speed);
        }

        if (_buttons != null)
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(true);
            }

        enabled = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
