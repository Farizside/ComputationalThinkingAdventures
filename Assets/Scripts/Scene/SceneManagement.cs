using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement: MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ChangeScene(string scene)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void RestartScene()
    {
        var last = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
        SceneManager.LoadScene(last, LoadSceneMode.Additive);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void UnloadLastScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
        FindObjectOfType<PlayerController>().isAbleToMove = true;
        StoryManager.Instance.MainCamera.gameObject.SetActive(true);
        AudioManager.Instance._bgmSource.mute = false;
        AudioManager.Instance.PlaySFX("Button");
    }

    public void OnFinishTask()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
        FindObjectOfType<PlayerController>().isAbleToMove = true;
        StoryManager.Instance.story.GetCurrentStage().isCompleted = true;
        StoryManager.Instance.MainCamera.gameObject.SetActive(true);
        AudioManager.Instance.PlayBGM("Inside");
        AudioManager.Instance.PlaySFX("Button");
    }

    public void FinishGame()
    {
        SaveSystem.SaveStory(StoryManager.Instance.story, "SaveFile.Json");
        AudioManager.Instance.PlaySFX("Button");
        SceneManager.LoadScene("MainMenu");
    }
}
