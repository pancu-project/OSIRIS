using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PauseButton pause;

    //����
    private bool isJump = false;
    public int JumpCount = 0;
    [SerializeField] float jumpForce=6f;
    [SerializeField] float gravityScaleDuringJump=1.5f; // ���� �� �߷� ������

    //�����̵�
    private bool isSlide = false;

    private Animator animator;
    //���ϸ����� ������Ʈ
    private Rigidbody2D playerRigidbody;
    //������ٵ� ������Ʈ


    private void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڰ� �浹 �� ���� �ʱ�ȭ
        if (collision.gameObject.tag == "bottom")
        {
            isJump = false;
            JumpCount = 0;
            animator.SetBool("isJump", false);
            playerRigidbody.gravityScale = 1f; // �߷� ������ �⺻������ ����
            animator.ResetTrigger("isDoubleJump");
            Debug.Log("Bottom!");
        }
    }
    private void Update()
    {
        if (!pause.IsTimeFlow()) { return; }

        //����
        if (Input.GetKeyDown(KeyCode.UpArrow) && JumpCount < 2)
        {
            JumpCount++;

            isJump = true;
            animator.SetBool("isJump", true); // ���� ���� ����
            playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Debug.Log("Jump!");

            playerRigidbody.gravityScale = gravityScaleDuringJump;

            if (JumpCount == 2 && isJump)
            {
                animator.SetTrigger("isDoubleJump");
                Debug.Log("Jump2!");
            }

        }

        //�����̵�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            StartSlide();
        }
        else if (isSlide)
        {
            EndSlide();
        }
    }

    private void StartSlide()//�����̵� ����
    {
        if (!isSlide)
        {
            isSlide = true;
            animator.SetBool("isSlide", true);
            Debug.Log("Slide!");
        }
    }

    private void EndSlide()//�����̵� ����
    {
        if (isSlide)
        {
            isSlide = false;
            animator.SetBool("isSlide", false);
            Debug.Log("Back to Run!");
        }
    }

}



