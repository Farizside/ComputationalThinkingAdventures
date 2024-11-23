using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story")]
public class StorySO : ScriptableObject
{
    public string name;
    public int curStage;
    public bool finish;
    public Stage[] stages;
    
    [Serializable]
    public class Stage
    {
        public bool isStarted;
        public bool isVideoPlayed;
        public bool isCompleted;
        public string onStartDialog;
        public string onProgressDialog;
        public string onCompleteDialog;
    }

    public Stage GetCurrentStage()
    {
        return stages[curStage];
    }

    public string GetCurrentDialog()
    {
        var stage = GetCurrentStage();
        if (!stage.isStarted && !stage.isCompleted)
        {
            stage.isStarted = true;
            return stage.onStartDialog;
        }
        
        if (stage.isStarted && !stage.isCompleted)
        {
            return stage.onProgressDialog;
        }

        if (stage.isStarted && stage.isCompleted)
        {
            return stage.onCompleteDialog;
        }

        return null;
    }
    
    public StoryData ToStoryData()
    {
        var storyData = new StoryData
        {
            name = name,
            curStage = curStage,
            finish = finish,
            stages = new List<StoryData.StageData>()
        };

        foreach (var stage in stages)
        {
            storyData.stages.Add(new StoryData.StageData
            {
                isStarted = stage.isStarted,
                isVideoPlayed = stage.isVideoPlayed,
                isCompleted = stage.isCompleted,
                onStartDialog = stage.onStartDialog,
                onProgressDialog = stage.onProgressDialog,
                onCompleteDialog = stage.onCompleteDialog
            });
        }

        return storyData;
    }
    
    public void UpdateFromStoryData(StoryData storyData)
    {
        name = storyData.name;
        curStage = storyData.curStage;
        finish = storyData.finish;

        // Pastikan jumlah stage cocok
        for (int i = 0; i < stages.Length; i++)
        {
            if (i < storyData.stages.Count)
            {
                stages[i].isStarted = storyData.stages[i].isStarted;
                stages[i].isVideoPlayed = storyData.stages[i].isVideoPlayed;
                stages[i].isCompleted = storyData.stages[i].isCompleted;
                stages[i].onStartDialog = storyData.stages[i].onStartDialog;
                stages[i].onProgressDialog = storyData.stages[i].onProgressDialog;
                stages[i].onCompleteDialog = storyData.stages[i].onCompleteDialog;
            }
        }
    }
}

[Serializable]
public class StoryData
{
    public string name;
    public int curStage;
    public bool finish;
    public List<StageData> stages;

    [Serializable]
    public class StageData
    {
        public bool isStarted;
        public bool isVideoPlayed;
        public bool isCompleted;
        public string onStartDialog;
        public string onProgressDialog;
        public string onCompleteDialog;
    }
}
