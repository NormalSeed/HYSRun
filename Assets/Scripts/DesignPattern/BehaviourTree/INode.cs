using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public interface INode
{
    public enum STATE
    { Success, Failure, Running }

    public INode.STATE Evaluate(); // 판단해서 상태 리턴
}

public class ActionNode : INode
{
    public Func<INode.STATE> action;
    public ActionNode(Func<INode.STATE> action)
    {
        this.action = action;
    }

    public INode.STATE Evaluate()
    {
        // 대리자가 null이 아닐 때 호출, null인 경우 Failure 반환
        return action?.Invoke() ?? INode.STATE.Failure;
    }
}

public class SelectorNode : INode
{
    List<INode> children; // 자식 노드들이 들어갈 리스트
    public SelectorNode()
    {
        children = new List<INode>();
    }

    public void Add(INode node) { children.Add(node); } // 자식 노드를 추가하는 메서드

    public INode.STATE Evaluate()
    {
        // 리스트 내의 노드들을 왼쪽부터(넣은 순서대로) 검사
        foreach(INode child in children)
        {
            INode.STATE state = child.Evaluate();
            // child 노드의 STATE가 하나라도 Success면 Success 반환
            // 실행 중인 경우 Running 반환
            switch (state)
            {
                case INode.STATE.Success:
                    return INode.STATE.Success;
                case INode.STATE.Running:
                    return INode.STATE.Running;
            }
        }
        // 반복문이 끝났다면 해당 셀렉터의 자식 노드들은 모두 Failure 상태이므로 셀렉터는 Failure 반환
        return INode.STATE.Failure;
    }
}

public class SequenceNode : INode
{
    List<INode> children; // 자식 노드를 넣을 리스트

    public SequenceNode() { children = new List<INode>(); }

    public void Add(INode node) { children.Add(node); }

    public INode.STATE Evaluate()
    {
        // 자식 노드의 수가 0 이하면 실패
        if (children.Count <= 0)
            return INode.STATE.Failure;

        foreach (INode child in children)
        {
            // 자식 노드들 중 하나라도 Failure면 Failure 반환
            switch (child.Evaluate())
            {
                case INode.STATE.Running:
                    return INode.STATE.Running;
                // Success면 아래는 검사하지 않고 continue
                case INode.STATE.Success:
                    continue;
                case INode.STATE.Failure:
                    return INode.STATE.Failure;
            }
        }
        // 반복문을 빠져나왔다면 Success 반환
        return INode.STATE.Success;
    }
}