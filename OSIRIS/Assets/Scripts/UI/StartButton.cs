using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel;

    public void StartButtonClicked()
    {
        if (!DataManager.Instance.currentData.isHowToShown)
        {
            howToPlayPanel.SetActive(true);
            DataManager.Instance.currentData.isHowToShown = true;
            DataManager.Instance.SaveData();
        }
        else
        {
            SceneManager.LoadScene("MapScene");
        }
    }

    private void Update()
    {
        if (howToPlayPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlayPanel.SetActive(false);
        }
    }
}
