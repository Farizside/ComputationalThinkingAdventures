using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookController : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
            StoryManager.Instance.MainCamera.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
