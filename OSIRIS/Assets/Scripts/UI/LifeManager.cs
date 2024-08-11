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
        // 죽었을 때
            // 목숨이 1개라면 게임 오버
            // 목숨이 2개 이상이라면 1개 차감

        // 시체 조각을 조건대로 회수했을 경우
            // 목숨 1 추가
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

                Debug.Log("충돌 하트 개수 ** : " + hearts.Count);

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
