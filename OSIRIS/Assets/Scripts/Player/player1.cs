using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player1 : MonoBehaviour
{
    public GameObject gameOverPanel;

    private PlayerAnimation playerAnimation;
    private PlayerMoving playerMoving;

    //��ü ����
    private DeadPartFull DeadPartFull1;
    private DeadPartFull DeadPartFull2;
    private int Deadcnt = 0;

    //��ü ���� ��ġ
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;

    //�������� ����

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMoving = GetComponent<PlayerMoving>();

        //��ü���� ������Ʈ
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
        
        if (transform.position.x >= 295f && Deadcnt == 0) // 1��° ��ü���� ȸ�� ���� �� ����
        {
            resetPosition1 = new Vector3(287f, -1.79f, transform.position.z);
            transform.position = resetPosition1;
            //playerMoving.heart = true;
            Debug.Log("��ü ���� 1 ȸ�� ����!!");
        }

        if (transform.position.x >= 545f && transform.position.y <= -2f && Deadcnt == 1) // 2��° ��ü���� ȸ�� ���� �� ����
        {
            resetPosition2 = new Vector3(532.51f, 1.19f, transform.position.z);
            transform.position = resetPosition2;
            
            Debug.Log("��ü ���� 2 ȸ�� ����!!");
        }

        if (transform.position.x >= 591f)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�߶� �� ��Ʈ ����
        if(collision.otherCollider == playerAnimation.foot)
         {
             if (collision.gameObject.CompareTag("repo1"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(323.3f, -2.71f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo2"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(335.35f, -1.74f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo3"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(370.8f, -2.76f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo4"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(386.53f, 0.33f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo5"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(398.27f, -2.73f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo6"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(461.33f, 1.36f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo7"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(478.48f, -2.78f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo8"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(510.2f, -0.78f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo9"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(514.37f, 1.24f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo10"))
             {
                 Debug.Log("�߶�!!");
                 playerMoving.moveSpeed = 0;
                 playerAnimation.minusHeart();

                 transform.position = new Vector3(522.18f, 2.23f, transform.position.z);
                 playerAnimation.reSpeed();
             }
             else if (collision.gameObject.CompareTag("repo11"))
             {
                 Debug.Log("�߶�!!");
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
