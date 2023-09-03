using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,EndGameObserve
{
    EnemyBaseState currentState;//��ǰ״̬
    public PatrolState patrolState = new PatrolState();//ʵ����Ѳ��״̬
    public AttackState attactState = new AttackState();//ʵ����Ѳ��״̬
    public WinState winState = new WinState();

    public Transform PointA, PointB;
    public Transform targetPoint;
    public Animator anim;
    public int animState;//���ƶ���״̬
    public GameObject TarG;

    public List<Transform> attackList = new List<Transform>();

    [Header("��������")]
    public float Speed;
    public float attackRate,skillattackRate;
    public float attackRange,skillRange;
    public float health;
    public float currentHealth;
    public bool isDead;

    protected float nextnormalAttack = 0, nextskillAttack = 0;  //��ͨ�����Լ����ܹ���
    public virtual void Init()//����������д
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
    public void TransitionToState(EnemyBaseState state)//�л�״̬
    {
        currentState = state;
        currentState.EnterState(this);//this��ʾʵ����������Ϊ���˼̳�Enemy��state�е���EnterState,this���Ǽ̳���Enemy��ʵ��
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
    public virtual void AttackAction()  //����
    {
        if(Vector2.Distance(targetPoint.position,transform.position) < attackRange)
        {
            if(nextnormalAttack <= 0)
            {
                //���Ŷ���
                Debug.Log("��ͨ����");
                anim.SetTrigger("attack");

                nextnormalAttack = attackRate;
            }
        }
    }

    public virtual void SkillAction()   //���⼼��
    {
        if (Vector2.Distance(targetPoint.position, transform.position) < skillRange)
        {
            if (nextskillAttack <= 0)
            {
                //���Ŷ���
                Debug.Log("���ܹ���");
                anim.SetTrigger("skill");
                nextskillAttack = skillattackRate;
            }
        }
    }

    public void FilpDirection() //��ת����
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
    void destroySelf()  //����֡�¼�
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
        if (QuestManager.IsInitialized && isDead)//IsInitialized��ʾʵ����
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
    public void EndNotify() //������Ϸ���㲥��enemy���������������½�һ��״̬��������ģ���״̬���и���
    {
        //�����ƶ�
        //���Ŷ���
        //ִ������
        TransitionToState(winState);
    }
}
