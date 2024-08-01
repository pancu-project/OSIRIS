using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PauseButton pause;

    //점프
    private bool isJump = false;
    public int JumpCount = 0;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravityScaleDuringJump = 5f; // 점프 중 중력 스케일

    //슬라이드
    private bool isSlide = false;

    //스킬-매로 변하기
    private bool isFly = false;
    private Coroutine transformationCoroutine;
    [SerializeField] private float FlyDuration = 3f;
    [SerializeField] private float FlyYPosition = -0.75f; // 변신 시 y 위치
    //[SerializeField] private float ComeBackYPosition = -2.8f;


    private Animator animator;
    //에니메이터 컴포턴트
    private Rigidbody2D playerRigidbody;
    //리지드바디 컴포넌트


    private void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 물약과 충돌 시 변신 시작
        if (other.CompareTag("potion") && !isFly)
        {
            StartTransformation();
            Destroy(other.gameObject); // 물약을 제거.
        }
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
        /*else
        {
            Vector3 newPosition = transform.position;
            newPosition.y = ComeBackYPosition;
            transform.position = newPosition;
        }*/
    }

    private void StartTransformation()
    {
   
        isFly = true;
        animator.SetBool("Flying", true);
        animator.SetTrigger("isFly");

        //Vector3 newPosition = transform.position;
        //newPosition.y = FlyYPosition;
        //transform.position = newPosition;

        if (transformationCoroutine != null)
        {
            StopCoroutine(transformationCoroutine);
        }

        transformationCoroutine = StartCoroutine(TransformationRoutine());
    }

    private IEnumerator TransformationRoutine()
    {
        // 3초 동안 변신 상태 유지
        yield return new WaitForSeconds(FlyDuration);
        EndTransformation();

    }

    private void EndTransformation()
    {

        isFly = false;
        animator.SetBool("Flying", false);
        animator.SetTrigger("BackRun");
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥과 충돌 시 점프 초기화
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isJump = false;
            JumpCount = 0;
            animator.SetBool("isJump", false);
            playerRigidbody.gravityScale = 1f; // 중력 스케일 기본값으로 리셋
            animator.ResetTrigger("isDoubleJump");
            Debug.Log("Bottom!");
        }
    }
    private void Update()
    {
        if (!pause.IsTimeFlow()) { return; }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2)
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
        if (Input.GetKey(KeyCode.DownArrow))
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
            Debug.Log("Slide!");
        }
    }

    private void EndSlide()//슬라이딩 종료
    {
        if (isSlide)
        {
            isSlide = false;
            animator.SetBool("isSlide", false);
            Debug.Log("Back to Run!");
        }
    }

}



