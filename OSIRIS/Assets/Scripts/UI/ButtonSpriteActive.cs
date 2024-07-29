using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteActive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite[] spriteAry;

    private Image buttonImage;
    public bool isButtonActive;

    private void Start()
    {
        buttonImage = GetComponent<Image>();

        if (buttonImage == null)
        {
            buttonImage.sprite = spriteAry[0];
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = spriteAry[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isButtonActive) buttonImage.sprite = spriteAry[0];
    }
}
