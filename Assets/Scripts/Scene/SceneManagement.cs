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
    }

    public void RestartScene()
    {
        var last = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
        SceneManager.LoadScene(last, LoadSceneMode.Additive);
    }

    public void UnloadLastScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
        FindObjectOfType<PlayerController>().isAbleToMove = true;
        StoryManager.Instance.MainCamera.gameObject.SetActive(true);
    }

    public void OnFinishTask()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
        FindObjectOfType<PlayerController>().isAbleToMove = true;
        StoryManager.Instance.story.GetCurrentStage().isCompleted = true;
        StoryManager.Instance.MainCamera.gameObject.SetActive(true);
    }

    public void FinishGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
