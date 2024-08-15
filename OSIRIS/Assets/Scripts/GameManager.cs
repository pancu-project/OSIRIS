using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string stage { get; set; } // �ʿ��� ������ �������� ���� ����

    private void Awake()
    {
        #region �̱���
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
    }
}
