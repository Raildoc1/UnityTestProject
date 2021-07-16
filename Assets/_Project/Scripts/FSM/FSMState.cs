using UnityEngine;

namespace TestGame.FSM
{
    public abstract class FSMState : IFSMState
    {
        protected BehaviourFSM BehaviourFSM;
        protected Transform Owner;

        public abstract StateType GetStateType();

        public virtual void Initialize(BehaviourFSM behaviourFSM, Transform owner)
        {
            BehaviourFSM = behaviourFSM;
            Owner = owner;
        }

        public virtual void Exit()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }
    }
}
