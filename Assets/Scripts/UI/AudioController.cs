using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private Toggle _toggle;

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    public void OnValueChanged()
    {
        if (_toggle.isOn)
        {
            _toggle.image.sprite = _onSprite;
        }
        else
        {
            _toggle.image.sprite = _offSprite;
        }
    }
}
