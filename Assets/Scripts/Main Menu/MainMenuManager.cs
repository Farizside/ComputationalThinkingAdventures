using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _newButton;
    [SerializeField] private GameObject _loadButton;
    [SerializeField] private StorySO _story;
    
    private void Start()
    {
        SaveSystem.LoadStory(_story, "SaveFile.Json");
        if (CheckSaveFile())
        {
            PlayerData.InputName(_story.name);
            _loadButton.SetActive(true);
        }
        else
        {
            _story.name = null;
            _story.finish = false;
            _story.curStage = 0;
            foreach (var stage in _story.stages)
            {
                stage.isCompleted = false;
                stage.isStarted = false;
                stage.isVideoPlayed = false;
            }
            
            SaveSystem.SaveStory(_story, "SaveFile.Json");
            _newButton.SetActive(true);
        }
        
        Debug.Log(PlayerData.Name);
    }

    private bool CheckSaveFile()
    {
        if (_story.finish || (_story.curStage == 0 && _story.stages[0].isStarted == false))
        {
            return false;
        }
        
        return true;
    }
}
