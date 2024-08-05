using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PauseButton pause;

    //����
    private bool isJump = false;
    public int JumpCount = 0;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravityScaleDuringJump = 5f; // ���� �� �߷� ������

    //�����̵�
    private bool isSlide = false;

    //��ų-�ŷ� ���ϱ�
    private bool isFly = false;
    private Coroutine transformationCoroutine;
    [SerializeField] private float FlyDuration = 3f;
    [SerializeField] private float FlyYPosition = -0.75f; // ���� �� y ��ġ
    //[SerializeField] private float ComeBackYPosition = -2.8f;


    private Animator animator;
    //���ϸ����� ������Ʈ
    private Rigidbody2D playerRigidbody;
    //������ٵ� ������Ʈ

    private Collider2D playerCollider; // �ݶ��̴� ������Ʈ �߰�
    private Collider2D slideCollider; // �����̵� �ݶ��̴� ������Ʈ �߰�
    private Collider2D jumpReset;// ���� �ʱ�ȭ �ݶ��̴� ������Ʈ �߰�

    private void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();

        playerCollider = GetComponent<Collider2D>(); // �ݶ��̴� ������Ʈ ��������
        slideCollider = transform.Find("SlidingCollide").GetComponent<Collider2D>();
        jumpReset = transform.Find("JumpReset").GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ����� �浹 �� ���� ����
        if (other.CompareTag("potion") && !isFly)
        {
            StartTransformation();
            Destroy(other.gameObject); // ������ ����.
        }
    }

    private void LateUpdate()
    {
        // Ư�� �ִϸ��̼� ���¿��� ��ġ ����
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

        playerCollider.enabled = false; // ���� ����� �ݶ��̴� ��Ȱ��ȭ
        slideCollider.enabled = false; // ���� ����� �ݶ��̴� ��Ȱ��ȭ
        jumpReset.enabled = false; // ���� ����� �ݶ��̴� ��Ȱ��ȭ

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
        // 3�� ���� ���� ���� ����
        yield return new WaitForSeconds(FlyDuration);
        EndTransformation();

    }

    private void EndTransformation()
    {

        isFly = false;
        animator.SetBool("Flying", false);
        animator.SetTrigger("BackRun");

        playerCollider.enabled = true; // ��ų ���� �� �ݶ��̴� Ȱ��ȭ
        slideCollider.enabled = true; //  ��ų ���� �� �ݶ��̴� Ȱ��ȭ
        jumpReset.enabled = true; // ��ų ���� �� �ݶ��̴� Ȱ��ȭ

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڰ� �浹 �� ���� �ʱ�ȭ
        if (collision.contacts[0].normal.y > 0.7f)
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
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2)
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

            playerCollider.enabled = false; // �����̵� ���� �� �ݶ��̴� ��Ȱ��ȭ

            Debug.Log("Slide!");
        }
    }

    private void EndSlide()//�����̵� ����
    {
        if (isSlide)
        {
            isSlide = false;
            animator.SetBool("isSlide", false);

            playerCollider.enabled = true; // �����̵� ���� �� �ݶ��̴� Ȱ��ȭ

            Debug.Log("Back to Run!");
        }
    }

}



