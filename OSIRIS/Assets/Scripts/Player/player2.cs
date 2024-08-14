using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player2 : MonoBehaviour
{
    public GameObject gameOverPanel;

    private PlayerAnimation playerAnimation;
    private PlayerMoving playerMoving;
    private LifeManager lifeManager;

    private bool isHeartAdded = false;

    //시체 조각
    private DeadPartFull DeadPartFull1;
    private DeadPartFull DeadPartFull2;
    private DeadPartFull DeadPartFull3;
    private DeadPartFull DeadPartFull4;
    private DeadPartFull DeadPartFull5;
    private int Deadcnt = 0;

    //시체 리셋 위치
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;
    private Vector3 resetPosition3;
    private Vector3 resetPosition4;
    private Vector3 resetPosition5;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMoving = GetComponent<PlayerMoving>();
        lifeManager = GameObject.Find("Life").GetComponent<LifeManager>();

        //시체조각 컴포넌트
        DeadPartFull1 = GameObject.Find("Dead1").GetComponent<DeadPartFull>();
        DeadPartFull2 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull3 = GameObject.Find("Dead3").GetComponent<DeadPartFull>();
        DeadPartFull4 = GameObject.Find("Dead4").GetComponent<DeadPartFull>();
        DeadPartFull5 = GameObject.Find("Dead5").GetComponent<DeadPartFull>();
    }

    public void ShowImage(int DeadCnt)
    {
        if (DeadPartFull2 != null && DeadCnt == 1)
        {
            Debug.Log("시체조각 팔 획득");
            Debug.Log(DeadCnt);
            DeadPartFull2.ShowDeadImage();
            isHeartAdded = false;
        }
        else if (DeadPartFull3 != null && DeadCnt == 2)
        {
            Debug.Log("**두번째 팔조각"+DeadCnt);
            DeadPartFull3.ShowDeadImage();
            if (!isHeartAdded)
            {
                lifeManager.TestAddButton();
                isHeartAdded = true; 
            }
        }
        else if (DeadPartFull4 != null && DeadCnt == 3)
        {
            DeadPartFull4.ShowDeadImage();
            isHeartAdded = false;
        }
        else if (DeadPartFull5 != null && DeadCnt == 4)
        {
            DeadPartFull5.ShowDeadImage();
            if (!isHeartAdded)
            {
                lifeManager.TestAddButton();
                isHeartAdded = true;
            }
        }
        else if (DeadPartFull1 != null && DeadCnt == 5)
        {
            DeadPartFull1.ShowDeadImage();
            isHeartAdded = false;
        }
    }

    private void Update()
    {
        Deadcnt = playerAnimation.Deadcnt;
        ShowImage(Deadcnt);
        
        if (transform.position.x >= 53f && transform.position.y <= -0.6f && Deadcnt == 0) // 1번째 시체조각 회수 못할 시 리셋
        {
            resetPosition1 = new Vector3(37.5f, 3.23f, transform.position.z);
          
            transform.position = resetPosition1;
           
            Debug.Log("시체 조각 1 회수 못함!!");
        }

        if (transform.position.x >= 357f && transform.position.y <= -2f && Deadcnt == 1) // 2번째 시체조각 회수 못할 시 리셋
        {
            resetPosition2 = new Vector3(344f, -0.79f, transform.position.z);
            
            transform.position = resetPosition2;
            Debug.Log("시체 조각 2 회수 못함!!");
        }

        if (transform.position.x >= 540f && transform.position.y <= -2f && Deadcnt == 2) // 3번째 시체조각 회수 못할 시 리셋
        {
            resetPosition3 = new Vector3(533f, -2.8f, transform.position.z);
            
            transform.position = resetPosition3;
            Debug.Log("시체 조각 3 회수 못함!!");
        }

        if (transform.position.x >= 610.8f && transform.position.y <= -2f && Deadcnt == 3) // 4번째 시체조각 회수 못할 시 리셋
        {
            resetPosition4 = new Vector3(600f, 2.22f, transform.position.z);
           
            transform.position = resetPosition4;
            Debug.Log("시체 조각 4 회수 못함!!");
        }

        if (transform.position.x >= 637f && transform.position.y <= -2f && Deadcnt == 4) // 5번째 시체조각 회수 못할 시 리셋
        {
            resetPosition5 = new Vector3(611f, -2.8f, transform.position.z);
          
            transform.position = resetPosition5;
            Debug.Log("시체 조각 5 회수 못함!!");
        }

        if(transform.position.x >= 691f)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //추락 시 하트 소진
        if (collision.otherCollider == playerAnimation.foot)
        {
            if (collision.gameObject.CompareTag("repo1"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(36f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo2"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(43.5f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo3"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(51f, -0.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo4"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(63f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo5"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(87f, -0.78f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo6"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(94f, 1.2f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo7"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(100f, -0.84f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo8"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(108.5f, 1.18f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo9"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(121.5f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo10"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(149.5f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo11"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(171.2f, -0.88f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo12"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(180.5f, 0.2f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo13"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(202f, 0.2f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo14"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(220.2f, 2.21f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo15"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(234.2f, -1.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo16"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(282.2f, 2.21f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo17"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(303.2f, -1.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo18"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(340.5f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo19"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(367f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo20"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(384f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo21"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(392.3f, 0.2f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo22"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(401f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo23"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(428.5f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo24"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(528.2f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo25"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(545.3f, 1.2f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo26"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(551.5f, 1.2f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo27"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(604.5f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo28"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(637f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo29"))
            {
                Debug.Log("추락!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(683.2f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            if (playerAnimation.LifeManager == null)
            {
                playerAnimation.GameOver();
            }
        }
    }
}
