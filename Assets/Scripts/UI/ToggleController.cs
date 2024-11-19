using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    private Toggle _toggle;
    private Image _image;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        _image = GetComponent<Image>();
        _toggle.onValueChanged.AddListener( OnToggleValueChanged ) ;
    }
    
    private void OnToggleValueChanged( bool isOn )
    {
        _image.color = isOn ? new Color(0.7f, 0.7f, 0.7f ) : new Color(1, 1, 1 ) ;
        if (isOn)
        {
            AudioManager.Instance.PlaySFX("Button");
        }
    }
}