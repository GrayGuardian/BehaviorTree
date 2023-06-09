using System.Data;
using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 序列节点
    /// 实现and逻辑
    /// 如果子节点状态为Success，则执行下一个子节点
    /// 如果子节点状态为Running，则返回Running，下次会继续执行接下来的子节点并等待返回其他结果（Success或Failed）
    /// 如果子节点状态为Failed，则返回Failed
    /// 如果所有节点都为Success，则返回Success
    /// </summary>
    public class SequenceNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.Sequence; } }
        private int _index;                 // 当前执行节点序号
        public SequenceNode(NodeBase[] childrens) : base()
        {
            SetChildrens(childrens);
            ResetParameter();
        }
        protected override void ResetParameter()
        {
            _index = 0;
        }
        public override void Visit()
        {
            base.Visit();
            int count = Childrens.Length;
            NodeBase node;
            while(_index < count)
            {
                node = Childrens[_index];
                if(node != null)
                {
                    node.Visit();
                    // 当子节点正在执行中或者失败，则打断等待下次顺序执行
                    if(node.Status == NodeStatus.Running || node.Status == NodeStatus.Failed)
                    {
                        SetStatus(node.Status);
                        return;
                    }
                }
                _index++;
            }
            SetStatus(NodeStatus.Success);
        }
    }
}