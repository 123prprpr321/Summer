using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;
    }

    public override void OnUpdate(Enemy enemy)
    {
        enemy.animState = 0;
    }
}
