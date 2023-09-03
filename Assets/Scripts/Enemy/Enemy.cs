using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,EndGameObserve
{
    EnemyBaseState currentState;//当前状态
    public PatrolState patrolState = new PatrolState();//实例化巡逻状态
    public AttackState attactState = new AttackState();//实例化巡逻状态
    public WinState winState = new WinState();

    public Transform PointA, PointB;
    public Transform targetPoint;
    public Animator anim;
    public int animState;//控制动画状态
    public GameObject TarG;

    public List<Transform> attackList = new List<Transform>();

    [Header("基本属性")]
    public float Speed;
    public float attackRate,skillattackRate;
    public float attackRange,skillRange;
    public float health;
    public float currentHealth;
    public bool isDead;

    protected float nextnormalAttack = 0, nextskillAttack = 0;  //普通攻击以及技能攻击
    public virtual void Init()//方便子类重写
    {
        anim = GetComponent<Animator>();
    }

    public void Awake()
    {
        Init();
    }
    void Start()
    {
        GameManager.Instance.AddObserver(this);
        TransitionToState(patrolState);
    }
    void Update()
    {
        anim.SetBool("dead", isDead);
        if(isDead)
        {
            return;
        }
        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);
        nextnormalAttack -= Time.deltaTime;
        nextskillAttack -= Time.deltaTime;
    }
    public void TransitionToState(EnemyBaseState state)//切换状态
    {
        currentState = state;
        currentState.EnterState(this);//this表示实例化对象，因为敌人继承Enemy，state中调用EnterState,this就是继承了Enemy的实例
    }
    public void MoveToTarget()
    {
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetPoint.position.x, Speed * Time.deltaTime), transform.position.y, transform.position.z);
        FilpDirection();
    }
    public void FlyEnemyMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, Speed * Time.deltaTime);
        FilpDirection();
    }
    public virtual void AttackAction()  //攻击
    {
        if(Vector2.Distance(targetPoint.position,transform.position) < attackRange)
        {
            if(nextnormalAttack <= 0)
            {
                //播放动画
                Debug.Log("普通攻击");
                anim.SetTrigger("attack");

                nextnormalAttack = attackRate;
            }
        }
    }

    public virtual void SkillAction()   //特殊技能
    {
        if (Vector2.Distance(targetPoint.position, transform.position) < skillRange)
        {
            if (nextskillAttack <= 0)
            {
                //播放动画
                Debug.Log("技能攻击");
                anim.SetTrigger("skill");
                nextskillAttack = skillattackRate;
            }
        }
    }

    public void FilpDirection() //翻转控制
    {
        if(transform.position.x < targetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void destroySelf()  //动画帧事件
    {
        //gameObject.SetActive(false);
        if(gameObject != null)
        Destroy(gameObject);
    }
    public void SwitchPoint()
    {
        if(Mathf.Abs(transform.position.x-PointA.position.x) > Mathf.Abs(transform.position.x - PointB.position.x))
        {
            targetPoint = PointA;
        }
        else
        {
            targetPoint = PointB;
        }
    }
    public void FlySwitchPoint()
    {
        Vector2 randomPos = new Vector2(Random.Range(PointA.position.x, PointB.position.x), Random.Range(PointA.position.y, PointB.position.y));
        TarG.transform.position = randomPos;
        targetPoint = TarG.transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!attackList.Contains(collision.transform))
        attackList.Add(collision.transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
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
    //void On
    //()
    //{
    //    GameManager.Instance.RemoveObserver(this);
    //    if (GetComponent<LootSpawner>() && isDead)
    //    {
    //        GetComponent<LootSpawner>().Spwanloot();
    //    }
    //}
    public void EndNotify() //结束游戏，广播，enemy做出反馈，这里新建一个状态，如需更改，在状态机中更改
    {
        //不再移动
        //播放动画
        //执行命令
        TransitionToState(winState);
    }
}
