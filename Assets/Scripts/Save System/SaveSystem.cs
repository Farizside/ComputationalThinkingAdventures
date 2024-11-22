using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string GetSavePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public static void SaveStory(StorySO story, string fileName)
    {
        var storyData = story.ToStoryData();
        var json = JsonUtility.ToJson(storyData, true);

        File.WriteAllText(GetSavePath(fileName), json);
        Debug.Log($"Story saved to {GetSavePath(fileName)}");
    }

    public static void LoadStory(StorySO story, string fileName)
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var loadedStoryData = JsonUtility.FromJson<StoryData>(json);

            if (loadedStoryData != null)
            {
                story.UpdateFromStoryData(loadedStoryData);
                Debug.Log("Story loaded and updated successfully.");
            }
        }
        else
        {
            Debug.LogWarning($"Save file not found at {path}");
        }
    }
}