using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel;
    private int clickCount = 0; // ���� Ŭ�� �� ���� ��� �Ұ�

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
