using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;
        if (enemy.CompareTag("Enemy"))
        {
            enemy.SwitchPoint();
        }
        else
        {
            enemy.FlySwitchPoint();
        }
       
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            enemy.animState = 1;
            if (enemy.CompareTag("Enemy"))
            {
                enemy.MoveToTarget();
            }
            else
            {
                enemy.FlyEnemyMove();
            }
        }
        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)
        {
            enemy.TransitionToState(enemy.patrolState);
        }
        if(enemy.attackList.Count>0)
        {
            enemy.TransitionToState(enemy.attactState);
        }
    }
}
