using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Buka");
        _animator.SetTrigger("Open");
        AudioManager.Instance.PlaySFX("Door");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Tutup");
        _animator.SetTrigger("Close");
        AudioManager.Instance.PlaySFX("Door");
    }
}
