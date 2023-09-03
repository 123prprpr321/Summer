using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfFlesh : MonoBehaviour, EndGameObserve, API
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;

    private bool isDead;
    public float health;
    public float currentHealth;
    public GameObject NextLevel;
    public GameObject NextPanel;
    public GameObject EnemyEye;
    private bool hasGenerated = false;
    public delegate void BeHit(float number);//����ί��
    public event BeHit HealthChange;//�����¼�

    
    private void Start()
    {
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.AddObserver(this);
    }
    private void Update()
    {
        checkHealth();
        if (currentHealth < 50 && !hasGenerated)
        {
            GenerateObject();
        }
        if (isDead)
        {
            moveSpeed = 0;
            Destroy(gameObject, 1.5f);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    private void OnDestroy()
    {
        //creatportals();
        //��һ�ط�ʽ��Ϊsetactive
    }
    void GenerateObject()
    {
        Instantiate(EnemyEye, transform.position + new Vector3(0, 6f, 0f), transform.rotation);
        Instantiate(EnemyEye, transform.position + new Vector3(0, 3f, 0f), transform.rotation);
        hasGenerated = true; // ���������ɱ�־Ϊ true
    }
    void creatportals()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 5f;
        GameObject Gate = Instantiate(NextLevel, spawnPosition, transform.rotation);
        SetActive gateScript = Gate.GetComponent<SetActive>();
        if (gateScript != null)
        {
            gateScript.targetObject = NextPanel;
        }
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
    
    void checkHealth()
    {
       if (35f < currentHealth && currentHealth <= 65f)
        {
            moveSpeed = 3.5f;
        }
       if (0 <= currentHealth && currentHealth <= 35f)
        {
            moveSpeed = 4.5f;
        }
    }
    public void GetHit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        float healthPercentage = currentHealth / health;//����ʣ��Ѫ���İٷֱ�
        if (HealthChange != null)
        {
            HealthChange(healthPercentage);
        }
    }

    public void ApplyHealth(float number)
    {

    }

    public void EndNotify()
    {
        moveSpeed = 0;
    }
}
