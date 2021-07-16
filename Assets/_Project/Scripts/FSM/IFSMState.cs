using UnityEngine;

namespace TestGame.FSM
{
    public interface IFSMState
    {
        StateType GetStateType();
        void Initialize(BehaviourFSM behaviourFSM, Transform owner);
        void Start();
        void Update();
        void LogicUpdate();
        void Exit();
    }
}