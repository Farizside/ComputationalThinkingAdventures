using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Toggle _toggle;
    private Animator _animator;

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    
    private static readonly int Highlighted = Animator.StringToHash("Highlighted");
    private static readonly int Normal = Animator.StringToHash("Normal");

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _animator = GetComponent<Animator>();
    }

    public void OnValueChanged()
    {
        AudioManager.Instance.PlaySFX("Button");
        AudioManager.Instance.Mute();
        if (_toggle.isOn)
        {
            _toggle.image.sprite = _onSprite;
        }
        else
        {
            _toggle.image.sprite = _offSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger(Highlighted);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger(Normal);
    }
}
