using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] Sprite[] gameEndSprite;
    private Image gameEndPanel;

    private void Start()
    {
        gameEndPanel = GetComponent<Image>();
    }

    public void SetGameClear()
    {
        gameEndPanel.sprite = gameEndSprite[0];
        this.gameObject.SetActive(true);
    }
}
