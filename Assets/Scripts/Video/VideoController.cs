using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject nextButton;

    void Start()
    {
        vid.loopPointReached += CheckOver;
    }

    void CheckOver(VideoPlayer vp)
    {
        nextButton.SetActive(true);
    }
}
