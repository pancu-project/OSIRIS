using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    PauseButton pause;
    
    //플레이어 속도 조절
    private PlayerMoving playerMoving;
    private SpriteRenderer sprite;
    Color originalColor;

    // 게임 오버 패널 추가
    public GameObject gameOverPanel;

    //시체 조각
    public int Deadcnt = 0;

    //하트 추가 및 삭제
    public LifeManager LifeManager;

    //점프 모션
    private bool isJump = false;
    public int JumpCount = 0;
    [Header("Jump")]
    [SerializeField] float jumpForce = 17f;
    [SerializeField] float doubleJumpForce = 15f;
    [SerializeField] float gravityScaleDuringJump = 5f; // 점프 중 중력 스케일

    //슬라이드 모션
    private bool isSlide = false;

    //스킬 UI 진행바
    private SkillProgressBar skillProgressBar; 

    //스킬 모션 - 매로 변신
    private bool isFly = false;
    private Coroutine transformationCoroutine;
    [Header("Fly")]
    [SerializeField] public float FlyDuration = 3f;
    [SerializeField] private float FlyYPosition = -0.75f; // 변신 시 y 위치

    //충돌 모션
    private bool isCollide = false;
    private bool trasition = false;

    // 충돌 후 무적 상태
    private bool isInvincible = false;
    [Header("Invincible")]
    [SerializeField] private float invincibleDuration = 2f; // 무적 상태 유지 시간

    //die 모션
    public bool isDie = false;

    private Animator animator; //에니메이터 컴포턴트
    private Rigidbody2D playerRigidbody; //리지드바디 컴포넌트

    //충돌 감지 콜라이더들
    public Collider2D playerCollider; // 전체 충돌
    public Collider2D slideCollider; // 슬라이드 충돌
    public Collider2D foot; // 점프 초기화 (바닥 충돌)

    private void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();

        //콜라이더 컴포넌트 
        playerCollider = transform.Find("totalColliding").GetComponent<Collider2D>(); 
        slideCollider = transform.Find("sliding").GetComponent<Collider2D>();
        foot = GetComponent<BoxCollider2D>();

        //하트(목숨) 컴포넌트
        LifeManager = GameObject.Find("Life").GetComponent<LifeManager>();

        //플레이어 무빙 스크립트 컴포넌트
        playerMoving = GetComponent<PlayerMoving>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        //스킬 UI 진행바 컴포넌트
        skillProgressBar = GameObject.Find("Skill Progress Gauge").GetComponent<SkillProgressBar>(); 
        skillProgressBar.falseActive(); // 비활성화

        // 정상으로 원상복귀
        Time.timeScale = 1f;

        playerRigidbody.gravityScale = gravityScaleDuringJump;
    }
    
    //충돌 감지 - 포션 감지 / 장애물 감지 (항아리,고양이,기둥) / 시체 조각 감지
    public void HandleTriggerCollision(string colliderRole, Collider2D other)
    {
        if (!isDie)
        {
            if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("potion") && !isFly) // 물약
            {
                //skillProgressBar.trueActive();
                StartTransformation();
                Destroy(other.gameObject);
            }

            if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Obstacle") && !isDie) // 장애물 
            {
                StartInvincibleState();
                Debug.Log("장애물이랑 충돌~");
                minusHeart();
            }

            if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Dead")) // 시체조각
            {
                Destroy(other.gameObject);
                Deadcnt++;
                Debug.Log("플레이션 애니: 시체조각" + Deadcnt);
            }
        }
    }

    public void minusHeart() // 하트 감소 함수
    {
        if (LifeManager != null)
        {
            LifeManager.TestDeleteButton();
        }
    }

    public void reSpeed() // 플레이어 속도 원상복구 함수
    {
        if (!isDie) // 죽지 않았을 때만
        {
            playerMoving.moveSpeed = 7f;
        }
    }

    public void GameOver() // 게임 오버 함수
    {
        isDie = true;
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
            playerMoving.moveSpeed = 4;
            isCollide = true;
            animator.SetBool("isCollide", true);
            sprite.color = new Color(1, 1, 1, 0.4f); //충돌 시 투명하게 바꾸기
            playerCollider.enabled = false; // 무적상태
            slideCollider.enabled = false;
            StartCoroutine(InvincibleRoutine());
        }
    }

    private IEnumerator InvincibleRoutine() // 충돌 시간
    {
        yield return new WaitForSeconds(invincibleDuration);
        if (!trasition)
        {
            EndInvincibleState();
        }
    }

    private void EndInvincibleState() //충돌 -> 달리기 모션 전환
    {
        isInvincible = false;
        isCollide = false;
        animator.SetBool("isCollide", false);
       
        reSpeed();
        playerCollider.enabled = true; // 무적상태 해제
        slideCollider.enabled = true;
        sprite.color = originalColor; //투명도 원래대로 복구
    }

    private void LateUpdate()
    {
        // die 애니 실행 후 게임 오버 띄우기
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("die"))
        {
            gameOverPanel.SetActive(true);
            pause.isEndingSceneAppear = true;
            Time.timeScale = 0f;
        }

        // 스킬 애니메이션 상태에서 위치 조정      
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

        //Debug.Log(FlyDuration);
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

    // 충돌 관리 - 점프카운트 리셋 / 추락 시 하트 소진
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == foot && collision.gameObject.CompareTag("bottom") && !isDie)
        {
            isJump = false;
            JumpCount = 0;
            animator.SetBool("isJump", false);
            animator.ResetTrigger("isDoubleJump");
            Debug.Log("Bottom!");
        }

        if (collision.otherCollider == slideCollider && collision.gameObject.CompareTag("bottom") && !isDie) // 장애물 
        {
            StartInvincibleState();
            minusHeart();
        }
    }

    private void Update()
    {
        if (!pause.IsTimeFlow()) { return; }
        if (isDie) { return; }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2 && isSlide == false)
        {
            isJump = true;
            animator.SetBool("isJump", true); // 점프 상태 설정

            if (JumpCount == 0)
            {
                playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            else if (JumpCount == 1)
            {
                playerRigidbody.AddForce(new Vector2(0, doubleJumpForce), ForceMode2D.Impulse);
            }

            JumpCount++;
            Debug.Log("Jump!");

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
            if(isCollide)
            {
                trasition = true;
                animator.SetBool("trasition", true);
                isInvincible = false;
                isCollide = false;
                animator.SetBool("isCollide", false);
                slideCollider.enabled = true;
            }
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
            if(trasition)
            {
                trasition = false;
                animator.SetBool("trasition", false);
                sprite.color = originalColor; //투명도 원래대로 복구
                reSpeed();
            }
            Debug.Log("Back to Run!");
        }
    }
}