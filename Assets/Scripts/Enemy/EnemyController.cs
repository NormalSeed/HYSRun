using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyModel model;
    private EnemyView view;

    public bool isInAttackRange;
    public bool isInDetectRange;
    private bool isTracing;

    private Transform target;

    private void Awake() => Init();

    protected virtual void Init()
    {
        model = GetComponent<EnemyModel>();
        view = GetComponent<EnemyView>();
    }

    protected virtual void FixedUpdate()
    {
        if (isTracing)
        {
            Trace();
        }
    }

    public virtual void Attack()
    {

    }

    public virtual void SetTargetDir()
    {
        target = GameObject.FindWithTag("Player").transform;
        isTracing = true;
    }

    private void Trace()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, model.MoveSpd *  Time.deltaTime);
    }

    public virtual void Patrol()
    {

    }
}
