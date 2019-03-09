using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVid : MonoBehaviour
{
    VideoPlayer video;
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!video.enabled)
            {
                video.enabled = true;
                video.Play();
            } else {
                video.enabled = false;
                video.Stop();
            }
        }
    }
}
