using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    PauseButton pause;

    public LayerMask Platform;

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
    public bool isFly = false;
    private Coroutine transformationCoroutine;
    [Header("Fly")]
    [SerializeField] public float FlyDuration = 3f;
    [SerializeField] private float FlyYPosition = -0.75f; // ���� �� y ��ġ

    //�浹 ���
    private bool isCollide = false;
    private bool trasition = false;
    private bool trasition2 = false;

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
        slideCollider.enabled = false;
        foot = GetComponent<BoxCollider2D>();

        slideCollider.enabled = false;

        //��Ʈ(���) ������Ʈ
        LifeManager = GameObject.Find("Life").GetComponent<LifeManager>();

        //�÷��̾� ���� ��ũ��Ʈ ������Ʈ
        playerMoving = GetComponent<PlayerMoving>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        //��ų UI ����� ������Ʈ
        skillProgressBar = GameObject.Find("Skill Progress Gauge").GetComponent<SkillProgressBar>(); 
        //skillProgressBar.falseActive(); // ��Ȱ��ȭ

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
        if (!trasition && !trasition2)
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
        SoundManager.Instance.PlaySFXSound("Skill");

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
      
    }

    // �浹 ���� - ����ī��Ʈ ���� / �߶� �� ��Ʈ ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == foot && collision.gameObject.CompareTag("bottom") && !isDie)
        {           
            isJump = false;
            animator.SetBool("isJump", false);
            JumpCount = 0;

            if (trasition2)
            {
                trasition2 = false;
                animator.SetBool("trasition2", false);
                isInvincible = false;
                sprite.color = originalColor; //���� ������� ����
                reSpeed();
                playerCollider.enabled = true;

            }

            Debug.Log("Bottom!");
        }
    }

    private void Update()
    {
        if (!pause.IsTimeFlow()) { return; }
        if (isDie) { return; }

        //raycast
        Vector3 raycastPosition = new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z);
        Vector3 raycastPosition2 = new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z);

        Debug.DrawRay(raycastPosition , Vector2.right * 0.8f, Color.red);
        Debug.DrawRay(raycastPosition2, Vector2.right * 0.8f, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.right,0.8f,LayerMask.GetMask("Platform"));
        {
            if(hit.collider!=null && !isInvincible && !isSlide && !isFly)
            {
                Debug.Log("����ĳ��Ʈ ����"+hit.collider.name);
                StartInvincibleState();
                minusHeart();
            }
        }

        RaycastHit2D hit2 = Physics2D.Raycast(raycastPosition2, Vector2.right, 0.8f, LayerMask.GetMask("Platform"));
        {
            if (hit2.collider != null && !isInvincible && !isFly)
            {
                Debug.Log("����ĳ��Ʈ ����" + hit2.collider.name);
                StartInvincibleState();
                minusHeart();
            }
        }


        //����
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2 && isSlide == false)
        {
            SoundManager.Instance.PlaySFXSound("Jump");

            if (isCollide)
            {
                trasition2 = true;
                animator.SetBool("trasition2", true);
                //isInvincible = false;
                isCollide = false;
                animator.SetBool("isCollide", false);               
            }

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
        if (Input.GetKey(KeyCode.S) && isJump == false && !isFly)
        {
            playerCollider.enabled = false;
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
            playerCollider.enabled = false; // �����̵� ���� �� �ݶ��̴� ��Ȱ��ȭ

            if (isCollide)
            {
                trasition = true;
                animator.SetBool("trasition", true);
                //isInvincible = false;
                isCollide = false;
                animator.SetBool("isCollide", false);

                isSlide = true;
                animator.SetBool("isSlide", true);               
               
                playerCollider.enabled = false; // �����̵� ���� �� �ݶ��̴� ��Ȱ��ȭ
            }
            else
            {
                isSlide = true;
                animator.SetBool("isSlide", true);

                slideCollider.enabled = true;
                playerCollider.enabled = false; // �����̵� ���� �� �ݶ��̴� ��Ȱ��ȭ
            }

            SoundManager.Instance.PlaySFXSound("Slide");
            Debug.Log("Slide!");
        }
    }

    private void EndSlide()//�����̵� ����
    {
        if (isSlide)
        {
            if (trasition)
            {
                trasition = false;
                isInvincible = false;
                animator.SetBool("trasition", false);
                sprite.color = originalColor; //���� ������� ����
                reSpeed();
            }

            isSlide = false;
            animator.SetBool("isSlide", false);
            slideCollider.enabled = false;
            playerCollider.enabled = true; // �����̵� ���� �� �ݶ��̴� Ȱ��ȭ

            
            Debug.Log("Back to Run!");
        }
    }
}