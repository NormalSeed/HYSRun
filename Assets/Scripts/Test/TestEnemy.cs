using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyController
{
    protected override void Start()
    {
        base.Start();
        model.MaxHP = 10;
        model.Attack = 1;
        model.MoveSpd = 2f;
        model.AttackRange = 1f;
        model.DetectRange = 5f;
        model.CurHP.Value = model.MaxHP;
    }
}
