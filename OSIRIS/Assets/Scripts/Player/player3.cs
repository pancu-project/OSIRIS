using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player3 : MonoBehaviour
{
    PauseButton pause;
    //플레이어 시체 조각 회수 못할 시 리셋 위치---------------수정해야하는 부분
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;
    private Vector3 resetPosition3;
    private Vector3 resetPosition4;
    private Vector3 resetPosition5;


    //플레이어 속도 조절
    private PlayerMoving playerMoving;

    //시체 조각----------------------------------------수정해야하는 부분
    public DeadPartFull DeadPartFull1;
    public DeadPartFull DeadPartFull2;
    public DeadPartFull DeadPartFull3;
    public DeadPartFull DeadPartFull4;
    public DeadPartFull DeadPartFull5;
    public int Deadcnt = 0;

    //하트 추가 및 삭제
    public LifeManager LifeManager;

    //점프 모션
    private bool isJump = false;
    public int JumpCount = 0;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravityScaleDuringJump = 5f; // 점프 중 중력 스케일

    //슬라이드 모션
    private bool isSlide = false;

    //스킬 UI 진행바
    private SkillProgressBar skillProgressBar;

    //스킬 모션 - 매로 변신
    private bool isFly = false;
    private Coroutine transformationCoroutine;
    [SerializeField] private float FlyDuration = 3f;
    [SerializeField] private float FlyYPosition = -0.75f; // 변신 시 y 위치

    //충돌 모션
    private bool isCollide = false;

    // 충돌 후 무적 상태
    private bool isInvincible = false;
    [SerializeField] private float invincibleDuration = 2f; // 무적 상태 유지 시간

    //die 모션

    private Animator animator; //에니메이터 컴포턴트
    private Rigidbody2D playerRigidbody; //리지드바디 컴포넌트

    //충돌 감지 콜라이더들
    private Collider2D playerCollider; // 전체 충돌
    private Collider2D slideCollider; // 슬라이드 충돌
    private Collider2D foot; // 점프 초기화 (바닥 충돌)



    private void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();

        //콜라이더 컴포넌트 
        playerCollider = transform.Find("totalColliding3").GetComponent<Collider2D>();
        slideCollider = transform.Find("sliding3").GetComponent<Collider2D>();
        foot = GetComponent<BoxCollider2D>();

        //시체조각 컴포넌트 -----------------------------------------------------------스테이지 2에 맞춰 수정해야하는 부분
        DeadPartFull1 = GameObject.Find("Dead1").GetComponent<DeadPartFull>();
        DeadPartFull2 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull3 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull4 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull5 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();

        //하트(목숨) 컴포넌트
        LifeManager = GameObject.Find("Life").GetComponent<LifeManager>();

        //플레이어 무빙 스크립트 컴포넌트
        playerMoving = GetComponent<PlayerMoving>();

        //스킬 UI 진행바 컴포넌트
        skillProgressBar = GameObject.Find("Skill Progress Gauge").GetComponent<SkillProgressBar>(); // 추가된 부분

    }

    //충돌 감지 - 포션 감지 / 장애물 감지 (항아리,고양이,기둥) / 시체 조각 감지
    public void HandleTriggerCollision(string colliderRole, Collider2D other)
    {
        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("potion") && !isFly) // 물약
        {
            StartTransformation();
            Destroy(other.gameObject);
        }

        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Obstacle")) // 장애물 
        {
            StartInvincibleState();
            if (LifeManager != null)
            {
                LifeManager.TestDeleteButton();
            }

        }

        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Dead")) // 시체조각
        {
            Destroy(other.gameObject);
            Deadcnt++;
            if (DeadPartFull1 != null && Deadcnt == 1)
            {
                DeadPartFull1.ShowDeadImage();
            }
            else if (DeadPartFull2 != null && Deadcnt == 2)
            {
                DeadPartFull2.ShowDeadImage();
            }
        }

    }

    public void HandleCollision(string colliderRole, Collider2D collider) // 벽이랑 충돌하면 하트 소진
    {
        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && collider.CompareTag("bottom")) // 장애물 
        {
            StartInvincibleState();
            if (LifeManager != null)
            {
                LifeManager.TestDeleteButton();
            }

        }
    }

    public void GameOver()
    {
        animator.SetBool("isDie", true);
        playerMoving.moveSpeed = 0;
        playerCollider.enabled = false;
        slideCollider.enabled = false;
    }

    private void StartInvincibleState()
    {
        if (!isInvincible) // 장애물 충돌 시 충돌 애니 실행
        {
            isInvincible = true;
            playerRigidbody.velocity = Vector2.zero; // 속도를 0으로 설정하여 정지
            playerMoving.moveSpeed = 4;
            isCollide = true;
            animator.SetBool("isCollide", true);
            playerCollider.enabled = false; // 무적상태
            slideCollider.enabled = false;
            StartCoroutine(InvincibleRoutine());
        }
    }

    private IEnumerator InvincibleRoutine() // 충돌 시간
    {
        yield return new WaitForSeconds(invincibleDuration);
        EndInvincibleState();
    }

    private void EndInvincibleState() //충돌 -> 달리기 모션 전환
    {
        isInvincible = false;
        isCollide = false;
        animator.SetBool("isCollide", false);
        playerMoving.moveSpeed = 7;
        playerCollider.enabled = true; // 무적상태 해제
        slideCollider.enabled = true;

    }

    private void LateUpdate()
    {
        // 특정 애니메이션 상태에서 위치 조정

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly"))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = FlyYPosition;
            transform.position = newPosition;
        }

    }

    private void StartTransformation() //변신 시작
    {

        isFly = true;
        animator.SetBool("Flying", true);
        animator.SetTrigger("isFly");

        playerCollider.enabled = false; // 물약 섭취시 콜라이더 비활성화
        slideCollider.enabled = false; // 물약 섭취시 콜라이더 비활성화

        if (transformationCoroutine != null)
        {
            StopCoroutine(transformationCoroutine);
        }

        skillProgressBar.StartProgressBar(FlyDuration);


        transformationCoroutine = StartCoroutine(TransformationRoutine());
    }

    private IEnumerator TransformationRoutine()//변신 유지 시간
    {
        // 3초 동안 변신 상태 유지
        yield return new WaitForSeconds(FlyDuration);
        EndTransformation();

    }

    private void EndTransformation()//변신 종료
    {

        isFly = false;
        animator.SetBool("Flying", false);
        animator.SetTrigger("BackRun");

        playerCollider.enabled = true; // 스킬 종료 후 콜라이더 활성화
        slideCollider.enabled = true; //  스킬 종료 후 콜라이더 활성화


    }

    // 충돌 관리 - 점프카운트 리셋 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == foot && collision.gameObject.CompareTag("bottom"))
        {
            isJump = false;
            JumpCount = 0;
            animator.SetBool("isJump", false);
            animator.ResetTrigger("isDoubleJump");
            Debug.Log("Bottom!");
        }

    }


    private void Update()
    {
        if (!pause.IsTimeFlow()) { return; }

        //시체 조각 회수 못할 시 리셋-------------------------------------------------------------수정해야하는 부분+시체 회수 시 하트 추가해야함
        if (transform.position.x >= 295f && Deadcnt == 0) // 1번째 시체조각 회수 못할 시 리셋
        {
            resetPosition1 = new Vector3(287f, -1.79f, transform.position.z);
            transform.position = resetPosition1;
            Debug.Log("시체 조각 1 회수 못함!!");
        }

        if (transform.position.x >= 545f && transform.position.y <= -2f && Deadcnt == 1) // 2번째 시체조각 회수 못할 시 리셋
        {
            resetPosition2 = new Vector3(532.51f, 1.19f, transform.position.z);
            transform.position = resetPosition2;
            Debug.Log("시체 조각 2 회수 못함!!");
        }

        //낭떨어지에 떨어지면 하트 소진----------------------------------------------------수정필요
        if (transform.position.y <= -6.5f)
        {
            if (LifeManager != null)
            {
                LifeManager.TestDeleteButton();
            }
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2 && isSlide == false)
        {
            JumpCount++;

            isJump = true;
            animator.SetBool("isJump", true); // 점프 상태 설정
            playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Debug.Log("Jump!");

            playerRigidbody.gravityScale = gravityScaleDuringJump;

            if (JumpCount == 2 && isJump)
            {
                animator.SetTrigger("isDoubleJump");
                Debug.Log("Jump2!");
            }

        }

        //슬라이딩
        if (Input.GetKey(KeyCode.S) && isJump == false)
        {
            StartSlide();
        }
        else if (isSlide)
        {
            EndSlide();
        }
    }

    private void StartSlide()//슬라이딩 시작
    {
        if (!isSlide)
        {
            isSlide = true;
            animator.SetBool("isSlide", true);

            playerCollider.enabled = false; // 슬라이딩 시작 시 콜라이더 비활성화

            Debug.Log("Slide!");
        }
    }

    private void EndSlide()//슬라이딩 종료
    {
        if (isSlide)
        {
            isSlide = false;
            animator.SetBool("isSlide", false);

            playerCollider.enabled = true; // 슬라이딩 종료 시 콜라이더 활성화

            Debug.Log("Back to Run!");
        }
    }
}
