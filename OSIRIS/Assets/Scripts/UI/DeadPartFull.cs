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
        DeadImage.enabled = true; // ��ü ���� �浹 �� UI �̹��� ���̰� ����
    }
}
