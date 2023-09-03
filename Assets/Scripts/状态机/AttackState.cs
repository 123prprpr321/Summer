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
        if(enemy.targetPoint.CompareTag("Player"))  //��ͬ�����Ӧ��Ҿ��벻ͬ���ɾ������ã����Ǹ�Ӧ֮��϶���ִ�й���������ֻʹ��attackAction�����Ǽ��ܹ�������������д
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
                enemy.FlyEnemyMove();//���÷��е���״̬
            }
        }
    }

}
