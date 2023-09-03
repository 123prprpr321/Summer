using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilities : MonoBehaviour
{
    private CheatsUIManager cheatsUIManager;//ʵ�������������¼�
    private PlayController player;
    public bool isAttacking;


    public float Icooldown;  // ��ȴʱ�䣨�룩
    private bool IisOnCoolDown = false;  // �����Ƿ�����ȴ״̬
    private float IcooldownTimer = 0f;  // ��ȴ��ʱ��
    private bool CEopen = false;

    public float Ocooldown;  // ��ȴʱ�䣨�룩
    private bool OisOnCoolDown = false;  // �����Ƿ�����ȴ״̬
    private float OcooldownTimer = 0f;  // ��ȴ��ʱ��
    [Header("˲�Ƽ���")]
    public GameObject Sword;
    public float coolDown;
    [Header("ϵͳ")]
    public GameObject Book;
    float timer;
    private GameObject item;

    [Header("�ɽ�����")]
    public GameObject leftSword;
    public GameObject rightSword;
    public GameObject Lpos;
    public GameObject Rpos;
    private GameObject closestEnemy;
    public float attackSpeed = 18f;
    private float detectionRadius = 15f;//�ɽ����˼�ⷶΧ

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
        isAttacking = false;
        timer = coolDown;
    }

    private void Start()
    {
        cheatsUIManager = FindObjectOfType<CheatsUIManager>();

        cheatsUIManager.CheckIsOpen += Cheatsdecide;//�����¼�
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

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);//����
            closestEnemy = null;
            float closestDistance = Mathf.Infinity;//�����(

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
            //���ﲻ�ö�������λ��ԭ������Ҫ�ƶ�λ�ã����ص�����غ�λ�ò��䣬���Ҳ�Ը����ж��Ƿ���е�bool
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

    IEnumerator MoveForwardForSeconds()//Э�̣�����I
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
        yield return new WaitForSeconds(0.314f); // �ȴ� 3 ��
        while (elapsedTime < 0.78f) //����ʱ��
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
    void StartCooldown()        // ���ü��ܽ�����ȴ״̬
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
                // ��ȴʱ����������ܽ����ʹ��״̬
                OisOnCoolDown = false;
            }
        }
    }

    void IattackCD()//����I��CD��ʱ��
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
