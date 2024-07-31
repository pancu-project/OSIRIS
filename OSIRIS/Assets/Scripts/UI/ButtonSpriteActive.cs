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
            SetBasicImage();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetActiveImage();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isButtonActive) SetBasicImage();
    }

    public void SetBasicImage()
    {
        buttonImage.sprite = spriteAry[0];
    }

    public void SetActiveImage()
    {
        buttonImage.sprite = spriteAry[1];
    }
}
