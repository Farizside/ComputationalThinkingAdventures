using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class EssayManager : MonoBehaviour
{
    [SerializeField] [CanBeNull] private List<TMP_InputField> _answers;
    [SerializeField] [CanBeNull] private List<string> _keyAnswers;
}
