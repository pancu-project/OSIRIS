using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioClip[] sfx_audioClips;
    [SerializeField] AudioClip[] bgm_audioClips;

    AudioSource bgm_player { get; set; }
    AudioSource sfx_player { get; set; }

    private void Awake()
    {
        #region ΩÃ±€≈Ê
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        bgm_player = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        sfx_player = GameObject.Find("SFX Player").GetComponent<AudioSource>();
    }

    private void Start()
    {
        bgm_player.volume = DataManager.Instance.currentData.volume;
        sfx_player.volume = DataManager.Instance.currentData.volume;
    }

    public void PlayBGMSound(string type)
    {
        int index = -1;

        switch (type)
        {
            case "StartScene":
                index = 0; break;
            case "MapScene":
                index = 0; break;
            case "PlayerSelectScene":
                index = 0; break;
            case "Stage1":
                index = 1; break;
            case "Stage2":
                index = 2; break;
            case "Stage3":
                index = 3; break;
        }

        if (index == -1) return;

        if (bgm_player.clip != bgm_audioClips[index] || bgm_player.clip == null)
        {
            bgm_player.clip = bgm_audioClips[index];
            bgm_player.Play();
        }
    }

    public void PlaySFXSound(string type)
    {
        int index = 0;

        switch (type)
        {
            case "Jump":
                index = 0; break;
            case "Slide":
                index = 1; break;
            case "Skill":
                index = 2; break;
            case "Sett":
                index = 3; break;
            case "UI":
                index = 4; break;
            case "Illust":
                index = 5; break;
        }

        sfx_player.clip = sfx_audioClips[index];
        sfx_player.PlayOneShot(sfx_player.clip);
    }

    public void StopBGMSound()
    {
        bgm_player.Stop();
    }
    public void StopSFXSound()
    {
        sfx_player.Stop();
    }
    
    public void ChangeVolume(float value)
    {
        bgm_player.volume = value;
        sfx_player.volume = value;
    }
}
