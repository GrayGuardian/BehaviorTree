using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 条件等待节点
    /// 根据函数调用结果返回Success或Running
    /// </summary>
    public class ConditionWaitNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.ConditionWait; } }
        protected Func<bool> _func;
        public ConditionWaitNode(Func<bool> func) : base()
        {
            _func = func;
        }
        public override void Visit()
        {
            base.Visit();
            if(_func!=null && _func())
                SetStatus(NodeStatus.Success);
            else
                SetStatus(NodeStatus.Running);
        }
    }
}