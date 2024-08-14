using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player3 : MonoBehaviour
{
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
    private DeadPartFull DeadPartFull6;
    private DeadPartFull DeadPartFull7;
    private int Deadcnt = 0;

    //시체 리셋 위치
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;
    private Vector3 resetPosition3;
    private Vector3 resetPosition4;
    private Vector3 resetPosition5;
    private Vector3 resetPosition6;
    private Vector3 resetPosition7;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMoving = GetComponent<PlayerMoving>();
        lifeManager = GameObject.Find("Life").GetComponent<LifeManager>();
    }
}
