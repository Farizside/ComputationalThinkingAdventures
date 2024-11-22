using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private string _message;
    [SerializeField] private GameObject _confirmPanel;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    private PlayerController _player;
    
    private CinemachineFreeLook _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<CinemachineFreeLook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _confirmPanel.SetActive(true);
            _text.text = _message;
            _yesButton.onClick.RemoveAllListeners();
            _noButton.onClick.RemoveAllListeners();
            _yesButton.onClick.AddListener(() => OnYesClicked(_sceneName));
            _noButton.onClick.AddListener(OnNoClicked);
            _player = other.GetComponent<PlayerController>();
            _player.isAbleToMove = false;
            _camera.enabled = false;
        }
    }

    private void OnYesClicked(string scene)
    {
        AudioManager.Instance.PlaySFX("Button");
        AudioManager.Instance.PlayBGM("Soal");
        _confirmPanel.SetActive(false);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        StoryManager.Instance.MainCamera.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnNoClicked()
    {
        AudioManager.Instance.PlaySFX("Button");
        _confirmPanel.SetActive(false);
        _player.isAbleToMove = true;
        _camera.enabled = true;
    }
}
