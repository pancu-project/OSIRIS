using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string stage { get; set; } // 맵에서 선택한 스테이지 정보 저장

    private void Awake()
    {
        #region 싱글톤
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
    }

    private void Start()
    {
        stage = "stage1";
        DataManager.Instance.LoadData();
        SceneManager.sceneLoaded += OnSceneLoaded;

        SoundManager.Instance.PlayBGMSound(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SoundManager.Instance.PlayBGMSound(SceneManager.GetActiveScene().name);
    }
}
