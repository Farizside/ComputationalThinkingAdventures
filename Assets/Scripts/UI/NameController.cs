using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _textField;

    public void OnNameChanged()
    {
        PlayerData.InputName(_textField.text);
    }
}
