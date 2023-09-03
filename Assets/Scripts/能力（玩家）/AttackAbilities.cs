using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilities : MonoBehaviour
{
    private CheatsUIManager cheatsUIManager;//实例，用来订阅事件
    private PlayController player;
    public bool isAttacking;


    public float Icooldown;  // 冷却时间（秒）
    private bool IisOnCoolDown = false;  // 技能是否处于冷却状态
    private float IcooldownTimer = 0f;  // 冷却计时器
    private bool CEopen = false;

    public float Ocooldown;  // 冷却时间（秒）
    private bool OisOnCoolDown = false;  // 技能是否处于冷却状态
    private float OcooldownTimer = 0f;  // 冷却计时器
    [Header("瞬移技能")]
    public GameObject Sword;
    public float coolDown;
    [Header("系统")]
    public GameObject Book;
    float timer;
    private GameObject item;

    [Header("飞剑技能")]
    public GameObject leftSword;
    public GameObject rightSword;
    public GameObject Lpos;
    public GameObject Rpos;
    private GameObject closestEnemy;
    public float attackSpeed = 18f;
    private float detectionRadius = 15f;//飞剑敌人检测范围

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
        isAttacking = false;
        timer = coolDown;
    }

    private void Start()
    {
        cheatsUIManager = FindObjectOfType<CheatsUIManager>();

        cheatsUIManager.CheckIsOpen += Cheatsdecide;//订阅事件
    }
    void Update()
    {
        if (CEopen)
        {
            return;
        }
        check();
        HSkill();
        transformChange();
        IattackCD();
        OattackCD();
        OpenBook();
    }

    void OpenBook()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Book != null)
            {
                Book.SetActive(!Book.activeSelf);
            }
        }
    }
    private void OnDestory()
    {
        cheatsUIManager.CheckIsOpen -= Cheatsdecide;
    }
    void Cheatsdecide(bool isActive)
    {
        CEopen = isActive;
    }
    void FlySword()
    {
 
            leftSword.transform.parent = null;
            rightSword.transform.parent = null;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);//数组
            closestEnemy = null;
            float closestDistance = Mathf.Infinity;//无穷大(

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy") || collider.CompareTag("FlyEnemy"))
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, collider.transform.position);
                    if (distanceToEnemy < closestDistance)
                    {
                        closestEnemy = collider.gameObject;
                        closestDistance = distanceToEnemy;
                        break;
                    }
                }
            }

            if (closestEnemy != null)
            {
                leftSword.SetActive(true);
                rightSword.SetActive(true);
                Vector3 leftSwordPosition = leftSword.transform.position;
                Vector3 rightSwordPosition = rightSword.transform.position;

                Vector3 leftSwordDirection = closestEnemy.transform.position - leftSwordPosition;
                Vector3 rightSwordDirection = closestEnemy.transform.position - rightSwordPosition;

                float leftSwordAngle = Mathf.Atan2(leftSwordDirection.y, leftSwordDirection.x) * Mathf.Rad2Deg;
                float rightSwordAngle = Mathf.Atan2(rightSwordDirection.y, rightSwordDirection.x) * Mathf.Rad2Deg;

                leftSword.transform.rotation = Quaternion.Euler(0, 0, leftSwordAngle);
                leftSword.transform.position = leftSwordPosition;

                rightSword.transform.rotation = Quaternion.Euler(0, 0, rightSwordAngle);
                rightSword.transform.position = rightSwordPosition; 

                StartCoroutine(MoveSwordsToTarget(closestEnemy.transform.position));

            }
    }

    IEnumerator MoveSwordsToTarget(Vector3 targetPosition)
    {
        Vector3 leftSwordTargetPosition = targetPosition;
        Vector3 rightSwordTargetPosition = targetPosition;

        while (Vector3.Distance(leftSword.transform.position, leftSwordTargetPosition) > 0.1f ||
            Vector3.Distance(rightSword.transform.position, rightSwordTargetPosition) > 0.1f)
        {
            leftSword.transform.position = Vector3.MoveTowards(leftSword.transform.position, leftSwordTargetPosition, attackSpeed * Time.deltaTime);
            rightSword.transform.position = Vector3.MoveTowards(rightSword.transform.position, rightSwordTargetPosition, attackSpeed * Time.deltaTime);
            yield return null;
        }

        leftSword.SetActive(false);
        rightSword.SetActive(false);

        leftSword.transform.SetParent(transform);
        rightSword.transform.SetParent(transform);


        leftSword.transform.position = Lpos.transform.position;
        rightSword.transform.position = Rpos.transform.position;
    }

    void HSkill()
    {
        timer += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.H) && timer >= coolDown)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0f, 1.2f, 0f);
            item = Instantiate(Sword, spawnPosition, transform.rotation);
            //这里不用多对象池是位置原因，我需要移动位置，但回到对象池后位置不变，并且不愿意加判断是否飞行的bool
            //item = ObjectPool.Instance.GetObject(Sword);
            //item.transform.position = transform.position + new Vector3(0f, 1.2f, 0f);
            timer = 0;
        }
    }

    void transformChange()
    {
        if(Input.GetKeyDown(KeyCode.H) && item != null)
        {
            transform.position = item.transform.position + new Vector3(0f, -1.2f, 0f);
        }
    }

    IEnumerator MoveForwardForSeconds()//协程，技能I
    {
        float elapsedTime = 0;
        Vector2 initialPosition = transform.position;
        Vector2 destinationPosition;
        if (player.transform.localScale.x == -1)
        {
            destinationPosition = (Vector2)transform.position + new Vector2(-1f, 0.0f) * 8;
        }
        else
        {
            destinationPosition = (Vector2)transform.position + new Vector2(1f, 0.0f) * 8;
        }
        yield return new WaitForSeconds(0.314f); // 等待 3 秒
        while (elapsedTime < 0.78f) //动画时长
        {
            transform.position = Vector2.Lerp(initialPosition, destinationPosition, elapsedTime / 0.78f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = destinationPosition;
        isAttacking = false;
    }
    private void check()
    {
        if (!IisOnCoolDown)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                isAttacking = true;
                Debug.Log(isAttacking);
                StartCoroutine("MoveForwardForSeconds");

                IisOnCoolDown = true;
                IcooldownTimer = Icooldown;
            }
        }

        if (!OisOnCoolDown && Input.GetKeyDown(KeyCode.O))
        {
            FlySword();
            StartCooldown();
        }

    }
    void StartCooldown()        // 设置技能进入冷却状态
    {
        OisOnCoolDown = true;
        OcooldownTimer = Ocooldown;
    }

    void OattackCD()
    {
        if (OisOnCoolDown)
        {
            OcooldownTimer -= Time.deltaTime;

            if (OcooldownTimer <= 0f)
            {
                // 冷却时间结束，技能进入可使用状态
                OisOnCoolDown = false;
            }
        }
    }

    void IattackCD()//技能I的CD计时器
    {
        if (IisOnCoolDown)
        {
            IcooldownTimer -= Time.deltaTime;

            if (IcooldownTimer <= 0f)
            {
                IisOnCoolDown = false;
            }
        }
    }

}
