using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class player2 : MonoBehaviour
{
    public GameObject gameOverPanel;

    private PlayerAnimation playerAnimation;
    private PlayerMoving playerMoving;
    private LifeManager lifeManager;

    private bool isHeartAdded = false;

    //��ü ����
    private DeadPartFull DeadPartFull1;
    private DeadPartFull DeadPartFull2;
    private DeadPartFull DeadPartFull3;
    private DeadPartFull DeadPartFull4;
    private DeadPartFull DeadPartFull5;
    private int Deadcnt = 0;

    //��ü ���� ��ġ
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;
    private Vector3 resetPosition3;
    private Vector3 resetPosition4;
    private Vector3 resetPosition5;

    //Ŭ���� ��ġ
    [SerializeField] Transform endPos;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMoving = GetComponent<PlayerMoving>();
        lifeManager = GameObject.Find("Life").GetComponent<LifeManager>();

        //��ü���� ������Ʈ
        DeadPartFull1 = GameObject.Find("Dead1").GetComponent<DeadPartFull>();
        DeadPartFull2 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull3 = GameObject.Find("Dead3").GetComponent<DeadPartFull>();
        DeadPartFull4 = GameObject.Find("Dead4").GetComponent<DeadPartFull>();
        DeadPartFull5 = GameObject.Find("Dead5").GetComponent<DeadPartFull>();

        //��������
        resetPosition1 = new Vector3(-5f, -2.8f, transform.position.z);
        resetPosition2 = new Vector3(313.5f, -2.8f, transform.position.z);
        resetPosition3 = new Vector3(462f, -2.8f, transform.position.z);
        resetPosition4 = new Vector3(560.5f, 1.2f, transform.position.z);
        resetPosition5 = new Vector3(608f, -2.8f, transform.position.z);
    }

    public void ShowImage(int DeadCnt)
    {
        if (DeadPartFull2 != null && DeadCnt == 1)
        {
            Debug.Log("��ü���� �� ȹ��");
            Debug.Log(DeadCnt);
            DeadPartFull2.ShowDeadImage();
            isHeartAdded = false;
        }
        else if (DeadPartFull3 != null && DeadCnt == 2)
        {
            Debug.Log("**�ι�° ������" + DeadCnt);
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

        if (transform.position.x >= 53f && transform.position.y <= -0.6f && Deadcnt == 0) // 1��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition1;
            Debug.Log("��ü ���� 1 ȸ�� ����!!");
        }
        if (transform.position.x >= 368f && transform.position.y <= -2f && Deadcnt == 1) // 2��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition2;
            Debug.Log("��ü ���� 2 ȸ�� ����!!");
        }
        if (transform.position.x >= 540f && transform.position.y <= -2f && Deadcnt == 2) // 3��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition3;
            Debug.Log("��ü ���� 3 ȸ�� ����!!");
        }
        if (transform.position.x >= 611f && transform.position.y <= -2f && Deadcnt == 3) // 4��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition4;
            Debug.Log("��ü ���� 4 ȸ�� ����!!");
        }
        if (transform.position.x >= 640f && transform.position.y <= -2f && Deadcnt == 4) // 5��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition5;
            Debug.Log("��ü ���� 5 ȸ�� ����!!");
        }

        if (transform.position.x >= endPos.position.x)
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
        //�߶� �� ��Ʈ ����
        if (collision.otherCollider == playerAnimation.foot)
        {
            if (collision.gameObject.CompareTag("repo1"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition1;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo2"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(51f, -0.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo3"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(122f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo4"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(248f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo5"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition2;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo6"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(375f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo7"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition3;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo8"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition4;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo9"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition5;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo10"))
            {
                Debug.Log("�߶�!!");
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = new Vector3(638f, -2.8f, transform.position.z);
                playerAnimation.reSpeed();
            }

            if (playerAnimation.LifeManager == null)
            {
                playerAnimation.GameOver();
            }
        }
    }
}
