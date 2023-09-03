using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public Transform player;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public float attackDuration = 1.5f; // 攻击持续时间
    public float laserFireRate = 0.6f; // 激光发射频率
    public float attackCooldown = 5f; // 攻击间隔时间

    private float rotationSpeed = 5f;
    private float number = 1f;//根据大小改变攻击状态
    private SpriteRenderer eyeRenderer;
    private Color color;

    private void Start()
    {
        WallOfFlesh wallOfFlesh = GetComponentInParent<WallOfFlesh>();//父级寻找事件
        if (wallOfFlesh != null)
        {
            wallOfFlesh.HealthChange += ChangeAttack;
        }
        eyeRenderer = GetComponent<SpriteRenderer>();
        color = eyeRenderer.color;
        UpdateAttack();
        laserFireRate = 0.6f;
        StartCoroutine(AttackCoroutine());
    }

    private void Update()//朝向玩家
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        UpdateAttack();
    }
    private void ChangeAttack(float healthPercentage)
    {
        number = healthPercentage;
    }    
    private void UpdateAttack()
    {
        if (0.75f <= number && number <= 1f)
        {
            laserFireRate = 0.6f;
        }
        else if (0.5f <= number && number < 0.75f)
        {
            laserFireRate = 0.45f;
            attackCooldown = 4.5f;
        }
        else if (0.2f <= number && number < 0.5f)
        {
            laserFireRate = 0.3f;
            attackDuration = 2.5f;
        }
        else
        {
            laserFireRate = 0.15f;
            attackCooldown = 4f;
        }
    }
    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCooldown);
            float attackTimer = 0f;
            while (attackTimer < attackDuration)
            {
                if (attackTimer % laserFireRate <= Time.deltaTime)
                {
                    FireLaser();
                }
                attackTimer += Time.deltaTime;
                yield return null;
            }
            eyeRenderer.color = color;

        }
    }

    private void FireLaser()
    {
        GameObject laser = ObjectPool.Instance.GetObject(laserPrefab);
        laser.transform.position = laserSpawnPoint.position;
        //GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
        if (eyeRenderer != null)
        {
            eyeRenderer.color = new Color(0.811f, 0.317f, 0.953f);
        }
    }
}
