using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected EnemyModel model;
    protected EnemyView view;

    // 상태 검사 관련 필드
    public bool isInAttackRange;
    public bool isInDetectRange;
    public EnemyState curState;

    // 이동 관련 필드
    protected GameObject player;
    protected Vector2 targetPos;
    protected int patrolDir;
    protected float changePatrolDirTimer;

    private void Awake() => Init();

    protected virtual void Init()
    {
        model = GetComponent<EnemyModel>();
        view = GetComponent<EnemyView>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        curState = EnemyState.Idle;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= model.AttackRange)
            isInAttackRange = true;
        else
            isInAttackRange = false;

        if (Vector2.Distance(transform.position, player.transform.position) <= model.DetectRange && Vector2.Distance(transform.position, player.transform.position) > model.AttackRange)
            isInDetectRange = true;
        else
            isInDetectRange = false;
    }

    protected virtual void FixedUpdate()
    {
        switch (curState)
        {
            case EnemyState.Trace:
                Trace();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
        }
    }

    public virtual void Attack()
    {

    }

    private void Trace()
    {
        targetPos = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, model.MoveSpd *  Time.deltaTime);
    }

    public virtual void Patrol()
    {
        if (changePatrolDirTimer <= 0f)
        {
            patrolDir = GetPatrolDir();
            changePatrolDirTimer = Random.Range(1f, 2f);
        }

        if (changePatrolDirTimer > 0f)
        {
            changePatrolDirTimer -= Time.deltaTime;
        }

        Vector3 move = Vector3.zero;
        switch (patrolDir)
        {
            case 0:
                // 왼쪽으로 이동
                move = Vector3.left;
                break;
            case 1:
                // 정지
                move = Vector3.zero;
                break;
            case 2:
                // 오른쪽으로 이동
                move = Vector3.right;
                break;
        }

        transform.position += move * model.MoveSpd * Time.deltaTime;
    }

    private int GetPatrolDir()
    {
        Debug.Log("새 순찰 방향 얻음");
        int dir = Random.Range(0, 3);
        return dir;
    }
}

public enum EnemyState
{
    Idle,
    Patrol,
    Trace,
    Attack,
    Stunned,
    Dead
}