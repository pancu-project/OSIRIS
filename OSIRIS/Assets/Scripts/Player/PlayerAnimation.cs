using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PauseButton pause;

    //점프
    private bool isJump = false;
    public int JumpCount = 0;
    [SerializeField] float jumpForce=6f;
    [SerializeField] float gravityScaleDuringJump=1.5f; // 점프 중 중력 스케일

    //슬라이드
    private bool isSlide = false;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥과 충돌 시 점프 초기화
        if (collision.gameObject.tag == "bottom")
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && JumpCount < 2)
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



