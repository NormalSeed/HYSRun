using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    SelectorNode rootNode;
    SequenceNode attackSequence;
    SequenceNode detectSequence;
    ActionNode idleAction;

    private EnemyController controller;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    protected virtual void Start()
    {
        // 공격 시퀀스 노드 생성
        attackSequence = new SequenceNode();
        attackSequence.Add(new ActionNode(CheckInAttackRange));
        attackSequence.Add(new ActionNode(Attack));

        // 탐지 시퀀스 노드 생성
        detectSequence = new SequenceNode();
        detectSequence.Add(new ActionNode(CheckInDetectRange));
        detectSequence.Add(new ActionNode(Trace));

        // 대기 액션 노드 생성
        idleAction = new ActionNode(Idle);

        // 루트 노드 생성
        rootNode = new SelectorNode();
        rootNode.Add(attackSequence);
        rootNode.Add(detectSequence);
        rootNode.Add(idleAction);
    }

    private void Update()
    {
        rootNode.Evaluate();
    }
    
    protected virtual INode.STATE CheckInAttackRange()
    {
        if (controller.isInAttackRange)
            return INode.STATE.Success;

        return INode.STATE.Failure;
    }

    protected virtual INode.STATE Attack()
    {
        controller.curState = EnemyState.Attack;
        controller.Attack();
        return INode.STATE.Running;
    }

    protected virtual INode.STATE CheckInDetectRange()
    {
        if (controller.isInDetectRange)
            return INode.STATE.Success;

        return INode.STATE.Failure;
    }

    protected virtual INode.STATE Trace()
    {
        controller.curState = EnemyState.Trace;
        return INode.STATE.Running;
    }

    protected virtual INode.STATE Idle()
    {
        controller.curState = EnemyState.Patrol;
        return INode.STATE.Running;
    }
}
