using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class player3 : MonoBehaviour
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
    private DeadPartFull DeadPartFull6;
    private DeadPartFull DeadPartFull7;
    private int Deadcnt = 0;

    //��ü ���� ��ġ
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;
    private Vector3 resetPosition3;
    private Vector3 resetPosition4;
    private Vector3 resetPosition5;
    private Vector3 resetPosition6;
    private Vector3 resetPosition7;


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
        DeadPartFull6 = GameObject.Find("Dead6").GetComponent<DeadPartFull>();
        DeadPartFull7 = GameObject.Find("Dead7").GetComponent<DeadPartFull>();

        //��������
        resetPosition1 = new Vector3(-5f, -2.8f, transform.position.z);
        resetPosition2 = new Vector3(32.5f, -2.8f, transform.position.z);
        resetPosition3 = new Vector3(198f, -2.8f, transform.position.z);
        resetPosition4 = new Vector3(290f, -2.8f, transform.position.z);
        resetPosition5 = new Vector3(379f, -2.8f, transform.position.z);
        resetPosition6 = new Vector3(457f, -2.8f, transform.position.z);
        resetPosition7 = new Vector3(525.5f, -2.8f, transform.position.z);
    }

    public void ShowImage(int DeadCnt)
    {
        if (DeadPartFull5 != null && DeadCnt == 1)
        {
            DeadPartFull5.ShowDeadImage();
            isHeartAdded = false;
        }
        else if (DeadPartFull6 != null && DeadCnt == 2)
        {
            DeadPartFull6.ShowDeadImage();
            isHeartAdded = false;
        }
        else if (DeadPartFull7 != null && DeadCnt == 3)
        {
            DeadPartFull7.ShowDeadImage();
            if (!isHeartAdded)
            {
                lifeManager.TestAddButton();
                isHeartAdded = true;
            }
        }
        else if (DeadPartFull2 != null && DeadCnt == 4)
        {
            DeadPartFull2.ShowDeadImage();
            isHeartAdded = false;
        }
        else if (DeadPartFull3 != null && DeadCnt == 5)
        {
            DeadPartFull3.ShowDeadImage();
            isHeartAdded = false;
        }
        else if (DeadPartFull4 != null && DeadCnt == 6)
        {
            DeadPartFull4.ShowDeadImage();
            if (!isHeartAdded)
            {
                lifeManager.TestAddButton();
                isHeartAdded = true;
            }
        }
        else if (DeadPartFull1 != null && DeadCnt == 7)
        {
            DeadPartFull1.ShowDeadImage();
            isHeartAdded = false;
        }
    }

    private void Update()
    {
        Deadcnt = playerAnimation.Deadcnt;
        ShowImage(Deadcnt);
        //��ü ���� ��ȹ�� �� ȸ��
        if (transform.position.x >= 33f && transform.position.y <= -2f && Deadcnt == 0) // 1��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition1;
        }
        if (transform.position.x >= 141f && transform.position.y <= -2f && Deadcnt == 1) // 2��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition2;
        }
        if (transform.position.x >= 263f && transform.position.y <= -2f && Deadcnt == 2) // 3��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition3;
        }
        if (transform.position.x >= 377.5f && transform.position.y <= -2f && Deadcnt == 3) // 4��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition4;
        }
        if (transform.position.x >= 458f && transform.position.y <= -2f && Deadcnt == 4) // 5��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition5;
        }
        if (transform.position.x >= 583f && transform.position.y <= -2f && Deadcnt == 5) // 4��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition6;
        }
        if (transform.position.x >= 653f && transform.position.y <= -2f && Deadcnt == 6) // 5��° ��ü���� ȸ�� ���� �� ����
        {
            transform.position = resetPosition7;
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
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition1;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo2"))
            {
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition2;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo3"))
            {
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition3;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo4"))
            {
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition4;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo5"))
            {
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition5;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo6"))
            {
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition6;
                playerAnimation.reSpeed();
            }
            else if (collision.gameObject.CompareTag("repo7"))
            {
                playerMoving.moveSpeed = 0;
                playerAnimation.minusHeart();
                transform.position = resetPosition7;
                playerAnimation.reSpeed();
            }          
        }
    }
}
