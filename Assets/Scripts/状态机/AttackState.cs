using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 2;
        enemy.targetPoint = enemy.attackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {

        if(enemy.attackList.Count == 0)
        {
            enemy.TransitionToState(enemy.patrolState);
        }
        if(enemy.attackList.Count == 1)
        {
            enemy.targetPoint = enemy.attackList[0];
        }
        if(enemy.targetPoint.CompareTag("Player"))  //不同怪物感应玩家距离不同，可具体设置，但是感应之后肯定是执行攻击，这里只使用attackAction，但是技能攻击放在重载里写
        {
            enemy.AttackAction();
        }
        else
        {
            enemy.AttackAction();
        }
        if (!enemy.anim.GetCurrentAnimatorStateInfo(1).IsName("attack") && !enemy.anim.GetCurrentAnimatorStateInfo(1).IsName("skill"))
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.MoveToTarget();
            }
            else
            {
                enemy.FlyEnemyMove();//设置飞行敌人状态
            }
        }
    }

}
