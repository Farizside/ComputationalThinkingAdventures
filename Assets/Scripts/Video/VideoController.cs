using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject nextButton;
    public GameObject restartButton;

    void Start()
    {
        vid.loopPointReached += CheckOver;
    }

    void CheckOver(VideoPlayer vp)
    {
        nextButton.SetActive(true);
        restartButton.SetActive(true);
    }
}
