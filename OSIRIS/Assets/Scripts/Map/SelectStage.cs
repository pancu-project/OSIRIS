using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField] GameObject blurBackground;
    [SerializeField] GameObject lockedStagePanel;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (sprite.enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (int.Parse(Regex.Replace(gameObject.name, @"[^0-9]", "")) <= GameManager.Instance.currentStage)
                {
                    GameManager.Instance.stage = gameObject.name;
                    SceneManager.LoadScene("PlayerSelectScene");
                }
                else
                {
                    lockedStagePanel.SetActive(true);
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (lockedStagePanel.activeSelf) return;

        blurBackground.SetActive(true);
        sprite.enabled = true;
    }

    private void OnMouseExit()
    {
        if (lockedStagePanel.activeSelf) return;

        blurBackground.SetActive(false);
        sprite.enabled = false;
    }
}
