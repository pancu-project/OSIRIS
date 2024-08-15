using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleSceneGauge : MonoBehaviour
{
    private Slider slider;
    private VideoPlayer openingAnimation;
    private bool isAnimationPlaying;

    [SerializeField] float timeSpeed = 0.2f;
    [SerializeField] GameObject openingPanel;
    [SerializeField] GameObject skipMessage;

    private void Start()
    {
        slider = GetComponent<Slider>();
        openingAnimation = openingPanel.GetComponent<VideoPlayer>();
        openingAnimation.prepareCompleted += OnVideoPrepared;

        SoundManager.Instance.PlaySFXSound("Illust");
    }

    private void Update()
    {
        slider.value += timeSpeed * Time.deltaTime;

        if (slider.value == 1 && !isAnimationPlaying)
        {
            openingAnimation.Prepare();
        }

        if (isAnimationPlaying && (!openingAnimation.isPlaying || Input.anyKeyDown))
        {
            SoundManager.Instance.StopSFXSound();
            SceneManager.LoadScene("StartScene");
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
        isAnimationPlaying = true;
        skipMessage.SetActive(true);

        Invoke("DeActiveSkipMessage", 3f);
    }

    private void DeActiveSkipMessage()
    {
        skipMessage.SetActive(false);
    }
}
