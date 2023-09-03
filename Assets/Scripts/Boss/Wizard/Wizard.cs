using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour, API
{
    private enum BossState
    {
        Phase1,
        Phase2
    }
    //观察者模式
    public delegate void BossDeathEventHandler();

    public static event BossDeathEventHandler BossDeathEvent;//静态事件不需要实例就能访问

    private BossState currentState;
    public float health;
    public float currentHealth;
    public ParticleSystem HitEffect;
    private SpriteRenderer sp;
    public Animator anim;
    public bool isDead;
    public Transform player;
    public GameObject AttackObject1;
    public GameObject AttackObject2;
    public GameObject AttackObject3;
    public GameObject AttackObject4;
    public GameObject AttackObject5;
    public Transform pos1;
    public Transform pos2;

    public float FlightDuration = 3f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        currentHealth = health;
        SwitchToPhase1();
    }
    // -0.17f ,4.65f  boss初始位置
    private void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            BossDeathEvent?.Invoke();
            return;
        }
        if (player.position.x <= transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void UpdateState()
    {
        if (currentHealth > 50f && currentState != BossState.Phase1)
        {
            SwitchToPhase1();
        }
        else if (currentHealth <= 50f && currentState != BossState.Phase2)
        {
            SwitchToPhase2();
        }
    }

    private void SwitchToPhase1()
    {
        StopAllCoroutines();
        currentState = BossState.Phase1;
        StartCoroutine(ReleaseSkillsABC());
    }

    private void SwitchToPhase2()
    {
        StopAllCoroutines();
        currentState = BossState.Phase2;
        StartCoroutine(ReleaseSkillsABCD());
    }

    private System.Collections.IEnumerator ReleaseSkillsABC()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            Debug.Log("Starting ReleaseSkillsABC");
            yield return StartCoroutine(ReleaseSkillA());
            yield return StartCoroutine(ReleaseSkillB());
            yield return StartCoroutine(ReleaseSkillC());
            Debug.Log("Finished ReleaseSkillsABC");
        }
    }

    private System.Collections.IEnumerator ReleaseSkillsABCD()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            Debug.Log("Starting ReleaseSkillsABCD");
            yield return StartCoroutine(ReleaseSkillA());
            yield return StartCoroutine(ReleaseSkillB());
            yield return StartCoroutine(ReleaseSkillB());
            yield return StartCoroutine(ReleaseSkillC());
            yield return StartCoroutine(ReleaseSkillD());
            yield return StartCoroutine(ReleaseSkillC());
            Debug.Log("Finished ReleaseSkillsABCD");
        }
    }

    private System.Collections.IEnumerator ReleaseSkillA()
    {
        transform.position = new Vector3(-0.17f, 4.65f, transform.position.z);
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("skill1");
        for (int i = 0; i < 4; i++)
        {
            //Instantiate(AttackObject1, new Vector3(player.transform.position.x, -4.82f + 0.737f, 0), player.rotation);
            GameObject temp = ObjectPool.Instance.GetObject(AttackObject1);
            temp.transform.position = new Vector3(player.transform.position.x, -4.82f + 0.737f, 0);
            temp.transform.rotation = player.rotation;

            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(1.75f);
    }

    private System.Collections.IEnumerator ReleaseSkillB()
    {
        anim.SetTrigger("skill1");
        Vector3 startPos1 = pos1.position;
        Vector3 startPos2 = pos2.position;

        int totalBullets = Mathf.Abs(2 - (-11));

        int bulletsPerLocation = totalBullets / 2;

        Vector3 chosenPosition; // 保存选定的位置

        if (Random.value < 0.5f)
        {
            chosenPosition = startPos1;
        }
        else
        {
            chosenPosition = startPos2;
        }

        for (int i = -11; i < 2; i++)
        {
            //GameObject ghost = Instantiate(AttackObject2, null);
            //Vector3 dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
            //ghost.transform.position = chosenPosition + dir * 1.0f;
            //ghost.transform.rotation = Quaternion.Euler(0, 0, i * 15);

            GameObject ghost = ObjectPool.Instance.GetObject(AttackObject2);
            Vector3 dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
            ghost.transform.position = chosenPosition + dir * 1.0f;
            ghost.transform.rotation = Quaternion.Euler(0, 0, i * 15);

            StartCoroutine(MoveBullet(ghost, dir));
        }

        yield return new WaitForSeconds(4.2f);
    }

    private System.Collections.IEnumerator ReleaseSkillC()
    {
        anim.SetTrigger("skill2");
        yield return new WaitForSeconds(1.6f);//等待动画
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        SpawnAttackObject3(transform.position);

        yield return new WaitForSeconds(0.5f);

        SpawnAttackObject3(new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z));
        SpawnAttackObject3(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z));

        yield return new WaitForSeconds(0.5f);

        SpawnAttackObject3(new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z));
        SpawnAttackObject3(new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z));

        yield return new WaitForSeconds(0.25f);

        SpawnAttackObject3(new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z));
        SpawnAttackObject3(new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z));

        yield return new WaitForSeconds(0.25f);

        SpawnAttackObject3(new Vector3(transform.position.x - 4.5f, transform.position.y, transform.position.z));
        SpawnAttackObject3(new Vector3(transform.position.x + 4.5f, transform.position.y, transform.position.z));

        void SpawnAttackObject3(Vector3 position)
        {
            //Instantiate(AttackObject3, position, Quaternion.Euler(0, 0, -135));
            GameObject newObject = ObjectPool.Instance.GetObject(AttackObject3);
            newObject.transform.position = position;
            newObject.transform.rotation = Quaternion.Euler(0, 0, -135);
        }

        yield return new WaitForSeconds(2f);
        anim.SetTrigger("move");
        yield return new WaitForSeconds(0.9f);

    }

    private System.Collections.IEnumerator ReleaseSkillD()
    {
        
        transform.position = new Vector3(-0.17f, 7f, transform.position.z);
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("skill2");
        yield return new WaitForSeconds(1.6f);//等待动画
        StartCoroutine(SpawnAttacks());
        StartCoroutine(NextAttack());
        yield return new WaitForSeconds(4f); 
    }
    private IEnumerator SpawnAttacks()
    {
        GameObject leftAttack = Instantiate(AttackObject4, new Vector3(-13.52f, -3.86f, 0f), Quaternion.identity);
        leftAttack.GetComponent<Rigidbody2D>().velocity = Vector2.right * 2;

        yield return new WaitForSeconds(FlightDuration);

        GameObject rightAttack = Instantiate(AttackObject4, new Vector3(13.49f, -3.86f, 0f), Quaternion.Euler(0f, 180f, 0f));
        rightAttack.GetComponent<Rigidbody2D>().velocity = Vector2.left * 2;
        yield return new WaitForSeconds(1.5f);

    }
    private IEnumerator NextAttack()
    {
        transform.position = new Vector3(-0.17f, -2.19f, transform.position.z);
        anim.SetTrigger("skill2");
        yield return new WaitForSeconds(1.6f);//等待动画
        GameObject rightAttack = Instantiate(AttackObject5, new Vector3(2.35f, -2.91f, 0f), Quaternion.Euler(0f, 180f, 0f));
        rightAttack.GetComponent<Rigidbody2D>().velocity = Vector2.left * 9;
        GameObject leftAttack = Instantiate(AttackObject5, new Vector3(-2.34f, -2.91f, 0f), Quaternion.identity);
        leftAttack.GetComponent<Rigidbody2D>().velocity = Vector2.right * 9;
        yield return new WaitForSeconds(4f);
    }
    public void GetHit(float damage)
    {
        hiteff();
        currentHealth -= damage;
        UpdateState();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
        Invoke("ResetColor", 0.33f);
    }
    private IEnumerator MoveBullet(GameObject bullet, Vector3 direction)
    {
        float speed = 5f;
        float timer = 0f;
        while (timer < 2.2f)
        {
            bullet.transform.Translate(direction * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        //Destroy(bullet);
        ObjectPool.Instance.PushObject(bullet);
    }
    void hiteff()
    {
        HitEffect.Play();
    }
    public void ResetColor()
    {
        sp.color = Color.white;
    }

    public void ApplyHealth(float number)
    {
       
    }
}
