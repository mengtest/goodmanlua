/********************************************************************************
** auth:  https://github.com/HushengStudent
** date:  2019/12/07 17:04:14
** desc:  ����״̬�����;
*********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class HumanStateMachineComponent : AbsComponent
    {
        public override string UID
        {
            get
            {
                return HashHelper.GetMD5(typeof(HumanStateMachineComponent).ToString());
            }
        }

        private AbsStateMachine _stateMachine;

        protected override void OnAttachGameObject(GameObjectEx goEx)
        {
            base.OnAttachGameObject(goEx);
            _stateMachine = StateMachineMgr.Instance.CreateStateMachine(Entity);
        }

        protected override void OnDetachGameObject()
        {
            base.OnDetachGameObject();
            _stateMachine = null;
            StateMachineMgr.Instance.RemoveStateMachine(Entity);
        }
    }
}