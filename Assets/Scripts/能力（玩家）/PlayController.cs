using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayController : MonoBehaviour,API
{
    public Rigidbody2D rb;//����
    private ScreenFlash sf;
    private Collider2D coll;//��ײ�壨���£�
    private WallMovement controller;
    private Animator anim;
    private CheatsUIManager cheatsUIManager;//ʵ�������������¼�


    [Header("��������")]
    public float speed;
    public float health;
    public float currentHealth;
    public bool isDead;
    public float jumpForce;
    public int jumpCountController;//���������Ծ����

    [Header("��������")]
    public bool isGround;//���ƶ���
    public bool isJump;//���ƶ���
    public Transform groundCheck;
    public LayerMask ground;
    public int jumpCount;//��Ծ����
    public float horizontalMove;//����
    private bool jumpPressed;//�Ƿ�����Ծ
    private SpriteRenderer myRender;
    private Color color;
    private bool CEopen = false;
    private float pauseTime = 0.2f;//��֡����
    private bool isPaused = false;

    [Header("��̲���")]
    public float dashSpeed;
    public float dashTime;
    public float dashCoolDown;
    public float dashTimeLeft;
    public float lastDash = -5f;
    public bool isDashing;

    [Header("��������")]
    public BoxCollider2D defenseCollider; // ��ɫ�ķ�����ײ��
    public bool Defenseing = false;  //����״̬
    private AttackAbilities Atk;

    public List<Buff> buffs = new List<Buff>();//ֻ��Ҫ����Ҫ���buff��������ʵ����Buff�б�Ȼ������������������������

    public GameObject Bow;


    //��������¼
    //I ���ܷ�Ծ  O ���ܷɽ�
    //H ����λ��  B ����
    //Q ����      R ����/�Ի�
    //space ��  enter ����
    private void Awake()
    {
        currentHealth = health;
    }
    void Start()
    {
        cheatsUIManager = FindObjectOfType<CheatsUIManager>();
        cheatsUIManager.CheckIsOpen += Cheatsdecide;//�����¼�

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        controller = GetComponent<WallMovement>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        myRender = GetComponent<SpriteRenderer>();
        Atk = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackAbilities>();
        color = myRender.color;


    }

    void Update()//�߼��жϺ���һ����updata
    {

        check();
        dieCheck();
        attackSwitch();
        Defence();
        UpdataBuff();

        if (SlotHolder.open == true)
        {
            Bow.SetActive(true);
        }
        else
        {
            Bow.SetActive(false);
        }
        if (isDead)
        {
            GameManager.Instance.NotifyObservers();
            Invoke("LoadNowScene", 5f);//�����ã��������¿�ʼ
        }
        if (isDead)
        {
            return;
        }

    }
    void LoadNowScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void FixedUpdate()//�˶�����һ����fixupdate
    {
        if(TalkUI.isOpen)
        {
            return;
        }
        if(CEopen)
        {
            return;
        }
        if(isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (Atk.isAttacking)
        {
            rb.velocity = Vector2.zero;
            anim.Play("Player-SpecialAttack");
            return;
        }
        if (Defenseing)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.125f, ground);//����Ƿ��ڵ���

        GroundMovement();

        Jump();

        Dash();
    }
    private void OnDestory()
    {
        cheatsUIManager.CheckIsOpen -= Cheatsdecide;
    }
    public void ApplyBuff(Buff buff)
    {
        buffs.Add(buff);
        switch (buff.Type)//���庯����ʵ��
        {
            case Buff.BuffType.Bleeding:
                Debug.Log("��Ѫ");
                StartCoroutine(Bleed(buff.Duration, buff.Amount));
                break;
            case Buff.BuffType.Healing:
                Debug.Log("�ظ�");
                StartCoroutine(Heal(buff.Duration, buff.Amount));
                break;
            case Buff.BuffType.Slowing:
                Debug.Log("����");
                StartCoroutine(Slow(buff.Duration, buff.Amount));
                break;
        }
    }
    private IEnumerator Bleed(float during, float amount)//��Ѫ
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < during)
        {
            currentHealth -= amount;
            elapsedTime += 1f;
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
    private IEnumerator Heal(float during, float amount)//�ظ�
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < during)
        {
            currentHealth += amount;
            elapsedTime += 1f;
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
    private IEnumerator Slow(float during, float amount)//����
    {
        float originalSpeed = speed;
        float originalDashSpeed = dashSpeed;
        
        speed *= amount;
        dashSpeed *= amount;

        yield return new WaitForSeconds(during);
        speed = originalSpeed;
        dashSpeed = originalDashSpeed;
    }
    void UpdataBuff()//����buff�б�
    {
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            Buff buff = buffs[i];

            buff.Duration -= 1.0f;

            switch (buff.Type)
            {
                case Buff.BuffType.Bleeding:
                    if (buff.Duration <= 0)
                    {
                        // ɾ�����ڵ�Buff
                        buffs.RemoveAt(i);
                        Debug.Log(123);
                    }
                    break;
                case Buff.BuffType.Healing:
                    break;
                case Buff.BuffType.Slowing:
                    break;
            }
        }
    }

    void Cheatsdecide(bool isActive)
    {
        CEopen = isActive;
    }
    void Defence()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Defense();
        }
    }
    private IEnumerator PauseGame()//ͨ�����ж���Ϸʵ��
    {
        isPaused = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void Defense()
    {
        anim.Play("Player-defence"); // ���ŷ�������
    }
    private void ActivateDefenseCollider()//����֡�¼�
    {
        defenseCollider.enabled = true;
        Defenseing = true;
    }
    private void CancelDefenseCollider()//����֡�¼�
    {
        defenseCollider.enabled = false;
        Defenseing = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (defenseCollider.enabled)
        {
            if (collision.CompareTag("Ememy Attack"))//unity����������
            {
                Debug.Log("����");
                if (!isPaused)
                {
                    StartCoroutine(PauseGame());
                }
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player-fall"))
        {
            if (collision.CompareTag("BounceSurface"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void check()
    {
        if(Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                
                ReadyToDash();
            }
        }
    }
    void dieCheck()
    {
        anim.SetBool("dead",isDead);
    }

    private void attackSwitch()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("attack");
        }
        anim.SetFloat("animTime", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
    }
    void Jump()
    {
        if(isGround)
        {
            rb.gravityScale = 1f;
            jumpCount = jumpCountController;
            isJump = false;
        }
        if(jumpPressed && (isGround||controller.isWallMove))
        {
            if (isGround)
            {
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                rb.gravityScale = 5.2f;
                jumpCount--;
                jumpPressed = false;
            }
            if (controller.isWallMove)//��ǽ��
            {
                if (controller.isRightWall)
                {
                    isJump = true;
                    rb.velocity = new Vector2(-40f, jumpForce * 1.15f);
                    rb.gravityScale = 5.2f;
                    jumpCount--;
                    jumpPressed = false;
                }
                else
                {
                    isJump = true;
                    rb.velocity = new Vector2(40f, jumpForce * 1.15f);
                    rb.gravityScale = 5.2f;
                    jumpCount--;
                    jumpPressed = false;
                }
            }
        }
        else if(jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }
    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }
    void Dash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                if (rb.velocity.y > 0 && !isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalMove, 0);
                }
                rb.velocity = new Vector2(dashSpeed * horizontalMove, 0);
                dashTimeLeft -= Time.deltaTime;
                //��Ӱ
                ShadowPool.instance.GetFormPool();
            }
            else if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }

    }

    public void OnDrawGizmos()//��ͼ�Σ�����Ҫ����
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.125f);
    }

    public void GetHit(float damage)
    {
        sf.FlashScreen();
        myRender.color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        currentHealth -= damage;
        if(currentHealth<=0)
        {
            currentHealth = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
        Invoke("ResetColor", 0.525f);   //��ʱ����
    }
    public void ResetColor()
    {
        myRender.color = color;
    }

    #region Apply Data Of Player    
    //��������޸�
    public void ApplyHealth(float number)
    {
        if (currentHealth + number <= health)
            currentHealth += number;
        else
            currentHealth = health;
    }
    #endregion
}
