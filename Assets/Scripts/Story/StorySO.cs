using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story")]
public class StorySO : ScriptableObject
{
    public int curStage;
    public Stage[] stages;
    
    [Serializable]
    public class Stage
    {
        public bool isStarted;
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
}
