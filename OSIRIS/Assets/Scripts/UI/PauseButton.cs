using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite[] pauseImage;
    [SerializeField] GameObject pausePanel;

    private Image buttonImage;
    private bool isTimeFlow = true;

    private void Start()
    {
        buttonImage = GetComponent<Image>();

        if (buttonImage == null)
        {
            buttonImage.sprite = pauseImage[0];
        }
    }

    public void PauseButtonClicked()
    {
        if (isTimeFlow)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            buttonImage.sprite = pauseImage[1];
            isTimeFlow = false;
        }
    }

    public void CloseButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");

        /*pausePanel.SetActive(false);
        Time.timeScale = 1f;
        buttonImage.sprite = pauseImage[0];
        isTimeFlow = true;*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isTimeFlow)
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                buttonImage.sprite = pauseImage[1];
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
                buttonImage.sprite = pauseImage[0];
            }

            isTimeFlow = !isTimeFlow;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = pauseImage[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isTimeFlow) buttonImage.sprite = pauseImage[0];
    }

    public bool IsTimeFlow() { return isTimeFlow; }
}
