using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayController : MonoBehaviour,API
{
    public Rigidbody2D rb;//刚体
    private ScreenFlash sf;
    private Collider2D coll;//碰撞体（脚下）
    private WallMovement controller;
    private Animator anim;
    private CheatsUIManager cheatsUIManager;//实例，用来订阅事件


    [Header("基本属性")]
    public float speed;
    public float health;
    public float currentHealth;
    public bool isDead;
    public float jumpForce;
    public int jumpCountController;//控制最大跳跃次数

    [Header("其他属性")]
    public bool isGround;//控制动画
    public bool isJump;//控制动画
    public Transform groundCheck;
    public LayerMask ground;
    public int jumpCount;//跳跃次数
    public float horizontalMove;//朝向
    private bool jumpPressed;//是否按下跳跃
    private SpriteRenderer myRender;
    private Color color;
    private bool CEopen = false;
    private float pauseTime = 0.2f;//顿帧长度
    private bool isPaused = false;

    [Header("冲刺参数")]
    public float dashSpeed;
    public float dashTime;
    public float dashCoolDown;
    public float dashTimeLeft;
    public float lastDash = -5f;
    public bool isDashing;

    [Header("防御技能")]
    public BoxCollider2D defenseCollider; // 角色的防御碰撞体
    public bool Defenseing = false;  //防御状态
    private AttackAbilities Atk;

    public List<Buff> buffs = new List<Buff>();//只需要在需要添加buff的物体上实例化Buff列表，然后调用添加下面两个函数即可

    public GameObject Bow;


    //按键备忘录
    //I 技能飞跃  O 技能飞剑
    //H 技能位移  B 背包
    //Q 任务      R 交互/对话
    //space 格挡  enter 作弊
    private void Awake()
    {
        currentHealth = health;
    }
    void Start()
    {
        cheatsUIManager = FindObjectOfType<CheatsUIManager>();
        cheatsUIManager.CheckIsOpen += Cheatsdecide;//订阅事件

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        controller = GetComponent<WallMovement>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        myRender = GetComponent<SpriteRenderer>();
        Atk = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackAbilities>();
        color = myRender.color;


    }

    void Update()//逻辑判断函数一般在updata
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
            Invoke("LoadNowScene", 5f);//测试用，死亡重新开始
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
    private void FixedUpdate()//运动函数一般在fixupdate
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
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.125f, ground);//检测是否处于地面

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
        switch (buff.Type)//具体函数中实现
        {
            case Buff.BuffType.Bleeding:
                Debug.Log("流血");
                StartCoroutine(Bleed(buff.Duration, buff.Amount));
                break;
            case Buff.BuffType.Healing:
                Debug.Log("回复");
                StartCoroutine(Heal(buff.Duration, buff.Amount));
                break;
            case Buff.BuffType.Slowing:
                Debug.Log("减速");
                StartCoroutine(Slow(buff.Duration, buff.Amount));
                break;
        }
    }
    private IEnumerator Bleed(float during, float amount)//流血
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < during)
        {
            currentHealth -= amount;
            elapsedTime += 1f;
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
    private IEnumerator Heal(float during, float amount)//回复
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < during)
        {
            currentHealth += amount;
            elapsedTime += 1f;
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
    private IEnumerator Slow(float during, float amount)//减速
    {
        float originalSpeed = speed;
        float originalDashSpeed = dashSpeed;
        
        speed *= amount;
        dashSpeed *= amount;

        yield return new WaitForSeconds(during);
        speed = originalSpeed;
        dashSpeed = originalDashSpeed;
    }
    void UpdataBuff()//更新buff列表
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
                        // 删除过期的Buff
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
    private IEnumerator PauseGame()//通过简单中断游戏实现
    {
        isPaused = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void Defense()
    {
        anim.Play("Player-defence"); // 播放防御动画
    }
    private void ActivateDefenseCollider()//动画帧事件
    {
        defenseCollider.enabled = true;
        Defenseing = true;
    }
    private void CancelDefenseCollider()//动画帧事件
    {
        defenseCollider.enabled = false;
        Defenseing = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (defenseCollider.enabled)
        {
            if (collision.CompareTag("Ememy Attack"))//unity里面打错字了
            {
                Debug.Log("免疫");
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
            if (controller.isWallMove)//蹬墙跳
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
                //残影
                ShadowPool.instance.GetFormPool();
            }
            else if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }

    }

    public void OnDrawGizmos()//画图形，不需要调用
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
        Invoke("ResetColor", 0.525f);   //延时调用
    }
    public void ResetColor()
    {
        myRender.color = color;
    }

    #region Apply Data Of Player    
    //玩家数据修改
    public void ApplyHealth(float number)
    {
        if (currentHealth + number <= health)
            currentHealth += number;
        else
            currentHealth = health;
    }
    #endregion
}
