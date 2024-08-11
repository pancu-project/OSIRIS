using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player3 : MonoBehaviour
{
    PauseButton pause;
    //�÷��̾� ��ü ���� ȸ�� ���� �� ���� ��ġ---------------�����ؾ��ϴ� �κ�
    private Vector3 resetPosition1;
    private Vector3 resetPosition2;
    private Vector3 resetPosition3;
    private Vector3 resetPosition4;
    private Vector3 resetPosition5;


    //�÷��̾� �ӵ� ����
    private PlayerMoving playerMoving;

    //��ü ����----------------------------------------�����ؾ��ϴ� �κ�
    public DeadPartFull DeadPartFull1;
    public DeadPartFull DeadPartFull2;
    public DeadPartFull DeadPartFull3;
    public DeadPartFull DeadPartFull4;
    public DeadPartFull DeadPartFull5;
    public int Deadcnt = 0;

    //��Ʈ �߰� �� ����
    public LifeManager LifeManager;

    //���� ���
    private bool isJump = false;
    public int JumpCount = 0;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravityScaleDuringJump = 5f; // ���� �� �߷� ������

    //�����̵� ���
    private bool isSlide = false;

    //��ų UI �����
    private SkillProgressBar skillProgressBar;

    //��ų ��� - �ŷ� ����
    private bool isFly = false;
    private Coroutine transformationCoroutine;
    [SerializeField] private float FlyDuration = 3f;
    [SerializeField] private float FlyYPosition = -0.75f; // ���� �� y ��ġ

    //�浹 ���
    private bool isCollide = false;

    // �浹 �� ���� ����
    private bool isInvincible = false;
    [SerializeField] private float invincibleDuration = 2f; // ���� ���� ���� �ð�

    //die ���

    private Animator animator; //���ϸ����� ������Ʈ
    private Rigidbody2D playerRigidbody; //������ٵ� ������Ʈ

    //�浹 ���� �ݶ��̴���
    private Collider2D playerCollider; // ��ü �浹
    private Collider2D slideCollider; // �����̵� �浹
    private Collider2D foot; // ���� �ʱ�ȭ (�ٴ� �浹)



    private void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();

        //�ݶ��̴� ������Ʈ 
        playerCollider = transform.Find("totalColliding3").GetComponent<Collider2D>();
        slideCollider = transform.Find("sliding3").GetComponent<Collider2D>();
        foot = GetComponent<BoxCollider2D>();

        //��ü���� ������Ʈ -----------------------------------------------------------�������� 2�� ���� �����ؾ��ϴ� �κ�
        DeadPartFull1 = GameObject.Find("Dead1").GetComponent<DeadPartFull>();
        DeadPartFull2 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull3 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull4 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();
        DeadPartFull5 = GameObject.Find("Dead2").GetComponent<DeadPartFull>();

        //��Ʈ(���) ������Ʈ
        LifeManager = GameObject.Find("Life").GetComponent<LifeManager>();

        //�÷��̾� ���� ��ũ��Ʈ ������Ʈ
        playerMoving = GetComponent<PlayerMoving>();

        //��ų UI ����� ������Ʈ
        skillProgressBar = GameObject.Find("Skill Progress Gauge").GetComponent<SkillProgressBar>(); // �߰��� �κ�

    }

    //�浹 ���� - ���� ���� / ��ֹ� ���� (�׾Ƹ�,�����,���) / ��ü ���� ����
    public void HandleTriggerCollision(string colliderRole, Collider2D other)
    {
        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("potion") && !isFly) // ����
        {
            StartTransformation();
            Destroy(other.gameObject);
        }

        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Obstacle")) // ��ֹ� 
        {
            StartInvincibleState();
            if (LifeManager != null)
            {
                LifeManager.TestDeleteButton();
            }

        }

        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Dead")) // ��ü����
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

    public void HandleCollision(string colliderRole, Collider2D collider) // ���̶� �浹�ϸ� ��Ʈ ����
    {
        if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && collider.CompareTag("bottom")) // ��ֹ� 
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
        if (!isInvincible) // ��ֹ� �浹 �� �浹 �ִ� ����
        {
            isInvincible = true;
            playerRigidbody.velocity = Vector2.zero; // �ӵ��� 0���� �����Ͽ� ����
            playerMoving.moveSpeed = 4;
            isCollide = true;
            animator.SetBool("isCollide", true);
            playerCollider.enabled = false; // ��������
            slideCollider.enabled = false;
            StartCoroutine(InvincibleRoutine());
        }
    }

    private IEnumerator InvincibleRoutine() // �浹 �ð�
    {
        yield return new WaitForSeconds(invincibleDuration);
        EndInvincibleState();
    }

    private void EndInvincibleState() //�浹 -> �޸��� ��� ��ȯ
    {
        isInvincible = false;
        isCollide = false;
        animator.SetBool("isCollide", false);
        playerMoving.moveSpeed = 7;
        playerCollider.enabled = true; // �������� ����
        slideCollider.enabled = true;

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

    }

    private void StartTransformation() //���� ����
    {

        isFly = true;
        animator.SetBool("Flying", true);
        animator.SetTrigger("isFly");

        playerCollider.enabled = false; // ���� ����� �ݶ��̴� ��Ȱ��ȭ
        slideCollider.enabled = false; // ���� ����� �ݶ��̴� ��Ȱ��ȭ

        if (transformationCoroutine != null)
        {
            StopCoroutine(transformationCoroutine);
        }

        skillProgressBar.StartProgressBar(FlyDuration);


        transformationCoroutine = StartCoroutine(TransformationRoutine());
    }

    private IEnumerator TransformationRoutine()//���� ���� �ð�
    {
        // 3�� ���� ���� ���� ����
        yield return new WaitForSeconds(FlyDuration);
        EndTransformation();

    }

    private void EndTransformation()//���� ����
    {

        isFly = false;
        animator.SetBool("Flying", false);
        animator.SetTrigger("BackRun");

        playerCollider.enabled = true; // ��ų ���� �� �ݶ��̴� Ȱ��ȭ
        slideCollider.enabled = true; //  ��ų ���� �� �ݶ��̴� Ȱ��ȭ


    }

    // �浹 ���� - ����ī��Ʈ ���� 
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

        //��ü ���� ȸ�� ���� �� ����-------------------------------------------------------------�����ؾ��ϴ� �κ�+��ü ȸ�� �� ��Ʈ �߰��ؾ���
        if (transform.position.x >= 295f && Deadcnt == 0) // 1��° ��ü���� ȸ�� ���� �� ����
        {
            resetPosition1 = new Vector3(287f, -1.79f, transform.position.z);
            transform.position = resetPosition1;
            Debug.Log("��ü ���� 1 ȸ�� ����!!");
        }

        if (transform.position.x >= 545f && transform.position.y <= -2f && Deadcnt == 1) // 2��° ��ü���� ȸ�� ���� �� ����
        {
            resetPosition2 = new Vector3(532.51f, 1.19f, transform.position.z);
            transform.position = resetPosition2;
            Debug.Log("��ü ���� 2 ȸ�� ����!!");
        }

        //���������� �������� ��Ʈ ����----------------------------------------------------�����ʿ�
        if (transform.position.y <= -6.5f)
        {
            if (LifeManager != null)
            {
                LifeManager.TestDeleteButton();
            }
        }

        //����
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2 && isSlide == false)
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
        if (Input.GetKey(KeyCode.S) && isJump == false)
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
