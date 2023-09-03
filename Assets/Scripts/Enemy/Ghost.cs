using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, EndGameObserve, API
{
    public Transform targetPoint;//只检测玩家位置
    public Animator anim;
    public int animState;//控制动画状态
    public GameObject BloodEffect;

    public GameObject PrefabOfAttack;
    public LayerMask playerLayer; // 玩家所在的层级

    [Header("基本属性")]
    public float skillattackRate;
    public float skillRange;
    public float health;
    public float currentHealth;
    public bool isDead;

    private float nextskillAttack = 0;
    private SpriteRenderer sp;
    public void Awake()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        currentHealth = health;
    }

    GameObject attackObject;
    void Start()
    {
        GameManager.Instance.AddObserver(this);

    }
    void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            return;
        }
        check();
        if (targetPoint != null)
        {
            if (Vector2.Distance(transform.position, targetPoint.position) <= skillRange && nextskillAttack <= 0)
            {
                SkillAttack();
            }
        }
        anim.SetInteger("state", animState);
        nextskillAttack -= Time.deltaTime;
    }

    void check()//检测玩家
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillRange, playerLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                targetPoint = collider.transform;
                break;
            }
        }
    }
    void SkillAttack()
    {
        Vector3 leftPoint = targetPoint.position + new Vector3(-4f, 1.2f, 0f);
        Vector3 rightPoint = targetPoint.position + new Vector3(4f, 1.2f, 0f);
        Vector3 movePosition = Random.value < 0.5f ? leftPoint : rightPoint;
        transform.position = movePosition;
        FilpDirection();
        anim.Play("ghost-skill");
        nextskillAttack = skillattackRate;
    }
    void CreatPrefab()//动画帧事件
    {
        attackObject = ObjectPool.Instance.GetObject(PrefabOfAttack);
        attackObject.transform.position = transform.position;
        if (transform.position.x > targetPoint.position.x)
            attackObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            attackObject.transform.localScale = new Vector3(1f, 1f, 1f);

    }
    public void EndNotify()
    {
        animState = 0;
    }
    public void FilpDirection() //翻转控制
    {
        if (transform.position.x < targetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void GetHit(float damage)
    {
        sp.color = Color.red;
      
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        Invoke("ResetColor", 0.33f);
    }
    public void ResetColor()
    {
        sp.color = Color.white;
    }
    public void ApplyHealth(float number)
    {

    }
    void OnDisable()
    {
        GameManager.Instance.RemoveObserver(this);
        if (GetComponent<LootSpawner>() && isDead)
        {
            GetComponent<LootSpawner>().Spwanloot();
        }
        if (QuestManager.IsInitialized && isDead)//IsInitialized表示实例化
        {
            QuestManager.Instance.UpdateQuestProgress(this.name, 1);
        }
    }
    public void destroySelf()
    {
        if (gameObject != null)
        Destroy(gameObject);
    }
}
