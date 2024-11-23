using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _textField;
    [SerializeField] private StorySO _story;

    public void OnNameChanged()
    {
        PlayerData.InputName(_textField.text);
        _story.name = PlayerData.Name;
    }
}
