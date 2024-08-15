using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    PauseButton pause;
    
    //�÷��̾� �ӵ� ����
    private PlayerMoving playerMoving;
    private SpriteRenderer sprite;
    Color originalColor;

    // ���� ���� �г� �߰�
    public GameObject gameOverPanel;

    //��ü ����
    public int Deadcnt = 0;

    //��Ʈ �߰� �� ����
    public LifeManager LifeManager;

    //���� ���
    private bool isJump = false;
    public int JumpCount = 0;
    [Header("Jump")]
    [SerializeField] float jumpForce = 17f;
    [SerializeField] float doubleJumpForce = 15f;
    [SerializeField] float gravityScaleDuringJump = 5f; // ���� �� �߷� ������

    //�����̵� ���
    private bool isSlide = false;

    //��ų UI �����
    private SkillProgressBar skillProgressBar; 

    //��ų ��� - �ŷ� ����
    private bool isFly = false;
    private Coroutine transformationCoroutine;
    [Header("Fly")]
    [SerializeField] public float FlyDuration = 3f;
    [SerializeField] private float FlyYPosition = -0.75f; // ���� �� y ��ġ

    //�浹 ���
    private bool isCollide = false;
    private bool trasition = false;

    // �浹 �� ���� ����
    private bool isInvincible = false;
    [Header("Invincible")]
    [SerializeField] private float invincibleDuration = 2f; // ���� ���� ���� �ð�

    //die ���
    public bool isDie = false;

    private Animator animator; //���ϸ����� ������Ʈ
    private Rigidbody2D playerRigidbody; //������ٵ� ������Ʈ

    //�浹 ���� �ݶ��̴���
    public Collider2D playerCollider; // ��ü �浹
    public Collider2D slideCollider; // �����̵� �浹
    public Collider2D foot; // ���� �ʱ�ȭ (�ٴ� �浹)

    private void Start()
    {
        pause = GameObject.Find("Pause").GetComponent<PauseButton>();
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();

        //�ݶ��̴� ������Ʈ 
        playerCollider = transform.Find("totalColliding").GetComponent<Collider2D>(); 
        slideCollider = transform.Find("sliding").GetComponent<Collider2D>();
        foot = GetComponent<BoxCollider2D>();

        //��Ʈ(���) ������Ʈ
        LifeManager = GameObject.Find("Life").GetComponent<LifeManager>();

        //�÷��̾� ���� ��ũ��Ʈ ������Ʈ
        playerMoving = GetComponent<PlayerMoving>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        //��ų UI ����� ������Ʈ
        skillProgressBar = GameObject.Find("Skill Progress Gauge").GetComponent<SkillProgressBar>(); 
        skillProgressBar.falseActive(); // ��Ȱ��ȭ

        // �������� ���󺹱�
        Time.timeScale = 1f;

        playerRigidbody.gravityScale = gravityScaleDuringJump;
    }
    
    //�浹 ���� - ���� ���� / ��ֹ� ���� (�׾Ƹ�,�����,���) / ��ü ���� ����
    public void HandleTriggerCollision(string colliderRole, Collider2D other)
    {
        if (!isDie)
        {
            if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("potion") && !isFly) // ����
            {
                //skillProgressBar.trueActive();
                StartTransformation();
                Destroy(other.gameObject);
            }

            if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Obstacle") && !isDie) // ��ֹ� 
            {
                StartInvincibleState();
                Debug.Log("��ֹ��̶� �浹~");
                minusHeart();
            }

            if ((colliderRole == "sildeCollider" || colliderRole == "totalCollider") && other.CompareTag("Dead")) // ��ü����
            {
                Destroy(other.gameObject);
                Deadcnt++;
                Debug.Log("�÷��̼� �ִ�: ��ü����" + Deadcnt);
            }
        }
    }

    public void minusHeart() // ��Ʈ ���� �Լ�
    {
        if (LifeManager != null)
        {
            LifeManager.TestDeleteButton();
        }
    }

    public void reSpeed() // �÷��̾� �ӵ� ���󺹱� �Լ�
    {
        if (!isDie) // ���� �ʾ��� ����
        {
            playerMoving.moveSpeed = 7f;
        }
    }

    public void GameOver() // ���� ���� �Լ�
    {
        isDie = true;
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
            playerMoving.moveSpeed = 4;
            isCollide = true;
            animator.SetBool("isCollide", true);
            sprite.color = new Color(1, 1, 1, 0.4f); //�浹 �� �����ϰ� �ٲٱ�
            playerCollider.enabled = false; // ��������
            slideCollider.enabled = false;
            StartCoroutine(InvincibleRoutine());
        }
    }

    private IEnumerator InvincibleRoutine() // �浹 �ð�
    {
        yield return new WaitForSeconds(invincibleDuration);
        if (!trasition)
        {
            EndInvincibleState();
        }
    }

    private void EndInvincibleState() //�浹 -> �޸��� ��� ��ȯ
    {
        isInvincible = false;
        isCollide = false;
        animator.SetBool("isCollide", false);
       
        reSpeed();
        playerCollider.enabled = true; // �������� ����
        slideCollider.enabled = true;
        sprite.color = originalColor; //���� ������� ����
    }

    private void LateUpdate()
    {
        // die �ִ� ���� �� ���� ���� ����
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("die"))
        {
            gameOverPanel.SetActive(true);
            pause.isEndingSceneAppear = true;
            Time.timeScale = 0f;
        }

        // ��ų �ִϸ��̼� ���¿��� ��ġ ����      
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

        //Debug.Log(FlyDuration);
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

    // �浹 ���� - ����ī��Ʈ ���� / �߶� �� ��Ʈ ����
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

        if (collision.otherCollider == slideCollider && collision.gameObject.CompareTag("bottom") && !isDie) // ��ֹ� 
        {
            StartInvincibleState();
            minusHeart();
        }
    }

    private void Update()
    {
        if (!pause.IsTimeFlow()) { return; }
        if (isDie) { return; }

        //����
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2 && isSlide == false)
        {
            isJump = true;
            animator.SetBool("isJump", true); // ���� ���� ����

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
            if(trasition)
            {
                trasition = false;
                animator.SetBool("trasition", false);
                sprite.color = originalColor; //���� ������� ����
                reSpeed();
            }
            Debug.Log("Back to Run!");
        }
    }
}