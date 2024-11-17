using System;
using System.Collections;
using System.Collections.Generic;
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

    public ObjectStage[] objectStages;

    public static StoryManager Instance;

    private PlayerController _player;

    private bool _isDialogCompleted
    {
        set
        {
            if (value == true)
            {
                OnDialogComplete();
            }
        }
    }

    [Serializable]
    public class ObjectStage
    {
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
    }

    public void StartDialog()
    {
        dialogCanvas.SetActive(true);
        var text = story.GetCurrentDialog();
        _isDialogCompleted = false;
        StartCoroutine(DisplayDialog(text));
    }

    IEnumerator DisplayDialog(string text)
    {
        dialogText.text = "";
        foreach (var c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(speedText);
        }
        
        yield return new WaitForSeconds(3);

        _isDialogCompleted = true;
    }

    public void OnDialogComplete()
    {
        _player.isAbleToMove = true;
        dialogCanvas.SetActive(false);
        dialogText.text = "";

        var curStage = story.GetCurrentStage();

        if (curStage.isStarted)
        {
            objectStages[story.curStage].openDoor.SetActive(true);
            objectStages[story.curStage].closeDoor.SetActive(false);

            if (objectStages[story.curStage].video != "")
            {
                _player.isAbleToMove = false;
                SceneManager.LoadScene(objectStages[story.curStage].video, LoadSceneMode.Additive);
                objectStages[story.curStage].video = "";
                MainCamera.gameObject.SetActive(false);
            }
        }
        
        if (curStage.isCompleted)
        {
            if (story.curStage <= 4)
            {
                medalImages[story.curStage].sprite = medalImage;
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
}
