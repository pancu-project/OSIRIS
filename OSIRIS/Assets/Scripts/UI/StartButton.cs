using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel;
    private int clickCount = 0; // 최초 클릭 시 게임 방법 소개

    public void StartButtonClicked()
    {
        if (++clickCount == 1)
        {
            howToPlayPanel.SetActive(true);
            return;
        }

        SceneManager.LoadScene("MapScene");
    }

    private void Update()
    {
        if (howToPlayPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlayPanel.SetActive(false);
        }
    }
}
