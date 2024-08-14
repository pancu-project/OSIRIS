using System.Collections;
using System.Collections.Generic;
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
            resetPosition1 = new Vector3(287f, -1.79f, transform.position.z);
            transform.position = resetPosition1;
            //playerMoving.heart = true;
            Debug.Log("시체 조각 1 회수 못함!!");
        }

        if (transform.position.x >= 545f && transform.position.y <= -2f && Deadcnt == 1) // 2번째 시체조각 회수 못할 시 리셋
        {
            resetPosition2 = new Vector3(532.51f, 1.19f, transform.position.z);
            transform.position = resetPosition2;
            
            Debug.Log("시체 조각 2 회수 못함!!");
        }

        if (transform.position.x >= 591f)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
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

                 transform.position = new Vector3(323.3f, -2.71f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo2"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(335.35f, -1.74f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo3"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(370.8f, -2.76f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo4"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(386.53f, 0.33f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo5"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(398.27f, -2.73f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo6"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(461.33f, 1.36f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo7"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(478.48f, -2.78f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo8"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(510.2f, -0.78f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo9"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(514.37f, 1.24f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo10"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(522.18f, 2.23f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo11"))
             {
                 Debug.Log("추락!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(549.47f, -2.71f, transform.position.z);
                 playerAnimation.reSpeed();
             }

             if (playerAnimation.LifeManager == null)
             {
                playerAnimation.GameOver();
             }
         }

    }
}
