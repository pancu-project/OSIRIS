using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] GameObject heart;
    private List<Image> hearts;

    private void Awake()
    {
        hearts = gameObject.GetComponentsInChildren<Image>().ToList<Image>();
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
        hearts = gameObject.GetComponentsInChildren<Image>().ToList<Image>();
        Destroy(hearts[hearts.Count - 1].gameObject);
    }

    public void TestAddButton()
    {
        hearts.Add(Instantiate(heart, this.transform).GetComponent<Image>());
        hearts = gameObject.GetComponentsInChildren<Image>().ToList<Image>();
    }
}
