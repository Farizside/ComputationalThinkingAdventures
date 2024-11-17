using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    [SerializeField] private bool _isAbletoInteract;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isAbletoInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isAbletoInteract = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (_isAbletoInteract)
            {
                _playerController.isAbleToMove = false;
                StoryManager.Instance.StartDialog();
                _isAbletoInteract = false;
            }
        }
    }
}
