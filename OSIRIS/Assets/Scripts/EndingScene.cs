using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndingScene : MonoBehaviour
{
    private VideoPlayer endingAnimation;
    private bool isAnimationPlaying;

    [SerializeField] GameObject endingPanel;

    private void Start()
    {
        endingAnimation = endingPanel.GetComponent<VideoPlayer>();
        endingAnimation.prepareCompleted += OnVideoPrepared;
    }

    private void Update()
    {
        if (!isAnimationPlaying)
        {
            endingAnimation.Prepare();
        }

        if (isAnimationPlaying && (!endingAnimation.isPlaying || Input.anyKeyDown))
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
        isAnimationPlaying = true;
    }
}
