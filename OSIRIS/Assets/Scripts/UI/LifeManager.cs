using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] GameObject heart;
    private List<Image> hearts;

    public PlayerAnimation PlayerAnimation;

    private void Awake()
    {
        hearts = gameObject.GetComponentsInChildren<Image>().ToList<Image>();
        
    }

    private void Start()
    {
        PlayerAnimation = GameObject.Find("Player").GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        // �׾��� ��
            // ����� 1����� ���� ����
            // ����� 2�� �̻��̶�� 1�� ����

        // ��ü ������ ���Ǵ�� ȸ������ ���
            // ��� 1 �߰�
    }

    public void TestDeleteButton()
    {
        if (hearts != null && hearts.Count > 0)
        {
            Image heartToRemove = hearts[hearts.Count - 1];
            if (heartToRemove != null)
            {
                Destroy(heartToRemove.gameObject);
                hearts.Remove(heartToRemove); 

                Debug.Log("�浹 ��Ʈ ���� ** : " + hearts.Count);

                if (hearts.Count == 0)
                {                    
                   PlayerAnimation.GameOver(); 
                }
            }
        }
    }
    public void TestAddButton()
    {
        hearts.Add(Instantiate(heart, this.transform).GetComponent<Image>());
        hearts = gameObject.GetComponentsInChildren<Image>().ToList<Image>();
    }
}
