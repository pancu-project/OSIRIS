using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class player1 : MonoBehaviour
{
    public GameObject gameOverPanel;

    private PlayerAnimation playerAnimation;
    private PlayerMoving playerMoving;

    //시체 조각
    private DeadPartFull DeadPartFull1;
    private DeadPartFull DeadPartFull2;
    private int Deadcnt = 0;

    //시체 리셋 위치
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;

    //낭떨어지 리셋

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMoving = GetComponent<PlayerMoving>();

        //시체조각 컴포넌트
        DeadPartFull1 = GameObject.Find("Dead1").GetComponent<DeadPartFull>();
        DeadPartFull2 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();

        resetPosition1 = new Vector3(238f, -2.8f, transform.position.z);
        resetPosition2 = new Vector3(371f, -2.8f, transform.position.z);
    }

    public void ShowImage(int DeadCnt)
    {
        if (DeadPartFull1 != null && DeadCnt == 1)
        {
            DeadPartFull1.ShowDeadImage();
        }
        else if (DeadPartFull2 != null && DeadCnt == 2)
        {
            DeadPartFull2.ShowDeadImage();
        }
    }

    private void Update()
    {
        Deadcnt = playerAnimation.Deadcnt;
        ShowImage(Deadcnt);

        if (transform.position.x >= 295f && Deadcnt == 0) // 1번째 시체조각 회수 못할 시 리셋
        {
            transform.position = resetPosition1;
            Debug.Log("시체 조각 1 회수 못함!!");
        }

        if (transform.position.x >= 545f && transform.position.y <= -2f && Deadcnt == 1) // 2번째 시체조각 회수 못할 시 리셋
        {
            transform.position = resetPosition2;
            Debug.Log("시체 조각 2 회수 못함!!");
        }

        if (transform.position.x >= 591f)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;

            PauseButton pause = GameObject.Find("Pause").GetComponent<PauseButton>();
            pause.isEndingSceneAppear = true;

            if (int.Parse(Regex.Replace(GameManager.Instance.stage, @"[^0-9]", "")) >= DataManager.Instance.currentData.stageLevel)
            {
                DataManager.Instance.currentData.stageLevel++;
                DataManager.Instance.SaveData();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //추락 시 하트 소진
        if(collision.otherCollider == playerAnimation.foot)
         {
             if (collision.gameObject.CompareTag("repo1"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(314.5f, -1.8f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo2"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = resetPosition2;
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo3"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(551f, -2.8f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             
             if (playerAnimation.LifeManager == null)
             {
                playerAnimation.GameOver();
             }
         }
    }
}
