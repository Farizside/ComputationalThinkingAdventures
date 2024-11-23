using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public Camera MainCamera;
    public StorySO story;
    [SerializeField] private GameObject dialogCanvas;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Image[] medalImages;
    [SerializeField] private Sprite medalImage;
    [SerializeField] private float speedText;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private Button _videoButton;
    [SerializeField] private GameObject exitPanel;

    public ObjectStage[] objectStages;

    public static StoryManager Instance;

    private PlayerController _player;

    private CinemachineFreeLook _camera;

    [SerializeField] private bool _isDialogCompleted = true;

    public bool IsDialogCompleted
    {
        get => _isDialogCompleted;
        set
        {
            _isDialogCompleted = value;
            if (value == true)
            {
                OnDialogComplete();
            }
        }  
    }

    [Serializable]
    public class ObjectStage
    {
        public GameObject book;
        public GameObject openDoor;
        public GameObject closeDoor;
        [CanBeNull] public string video;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _camera = FindObjectOfType<CinemachineFreeLook>();
        AudioManager.Instance.PlayBGM("Outside");
        SaveSystem.LoadStory(story, "SaveFile.Json");
        OnRespawn();
    }

    public void StartDialog()
    {
        dialogCanvas.SetActive(true);
        var text = story.GetCurrentDialog();
        IsDialogCompleted = false;
        StartCoroutine(DisplayDialog(text));
    }

    IEnumerator DisplayDialog(string text)
    {
        _camera.enabled = false;
        var curStage = story.GetCurrentStage();
        
        dialogText.text = "";
        foreach (var c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(speedText);
        }

        if (curStage.isStarted && !curStage.isVideoPlayed)
        {
            _videoButton.gameObject.SetActive(true);
            _videoButton.onClick.AddListener(OnDialogComplete);
            _videoButton.onClick.AddListener(ButtonSFX);
        }
        else
        {
            yield return new WaitForSeconds(3);
            
            IsDialogCompleted = true;
        }
    }

    public void OnDialogComplete()
    {
        _player.isAbleToMove = true;
        _camera.enabled = true;
        dialogCanvas.SetActive(false);
        dialogText.text = "";

        var curStage = story.GetCurrentStage();

        if (curStage.isStarted)
        {
            objectStages[story.curStage].openDoor.SetActive(true);
            objectStages[story.curStage].closeDoor.SetActive(false);

            if (!curStage.isVideoPlayed)
            {
                _player.isAbleToMove = false;
                _videoButton.gameObject.SetActive(false);
                SceneManager.LoadScene(objectStages[story.curStage].video, LoadSceneMode.Additive);
                curStage.isVideoPlayed = true;
                MainCamera.gameObject.SetActive(false);
                AudioManager.Instance._bgmSource.mute = true;
            }
            
            SaveSystem.SaveStory(story, "SaveFile.Json");
        }
        
        if (curStage.isCompleted)
        {
            if (story.curStage <= 4)
            {
                medalImages[story.curStage].sprite = medalImage;
                if (story.curStage == 4)
                {
                    AudioManager.Instance.PlaySFX("Evaluate");
                }
                else
                {
                    AudioManager.Instance.PlaySFX("Medal");
                }
            }

            if (story.curStage < 5)
            {
                story.curStage++;
            }
            else
            {
                finishPanel.SetActive(true);
                _player.isAbleToMove = false;
            }
            
        }
        
        StopAllCoroutines();
    }

    public void ButtonSFX()
    {
        AudioManager.Instance.PlaySFX("Button");
    }

    public void OnRespawn()
    {
        for (int i = 0; i < story.stages.Length; i++)
        {
            if (story.stages[i].isStarted)
            {
                objectStages[i].closeDoor.SetActive(false);
                objectStages[i].openDoor.SetActive(true);
            }

            if (story.stages[i].isCompleted)
            {
                objectStages[i].book.SetActive(false);
            }

            if (story.stages[i].isCompleted && story.curStage > i && i <= 4)
            {
                medalImages[i].sprite = medalImage;
            }
        }
    }

    public void OnExitButton()
    {
        _camera.enabled = false;
        _player.isAbleToMove = false;
        exitPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void OnCancelExit()
    {
        _camera.enabled = true;
        _player.isAbleToMove = true;
        exitPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
    }
}
