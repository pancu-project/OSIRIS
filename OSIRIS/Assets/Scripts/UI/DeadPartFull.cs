using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeadPartFull : MonoBehaviour
{
    private Image DeadImage;
    
    void Start()
    {
        DeadImage = GetComponent<Image>();
        DeadImage.enabled = false;
    }

    public void ShowDeadImage()
    {
        DeadImage.enabled = true; // 시체 조각 충돌 시 UI 이미지 보이게 설정
    }
}
