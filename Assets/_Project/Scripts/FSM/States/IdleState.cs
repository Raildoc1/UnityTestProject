using UnityEngine;

namespace TestGame.FSM
{
    [System.Serializable]
    public class IdleState : FSMState
    {
        private Animator _animator;
        private int _speedParamHash;

        [SerializeField] private string _speedParamName = "Speed";
        [SerializeField] private KeyCode _startWalkKey = KeyCode.Mouse0;

        public override StateType GetStateType()
        {
            return StateType.Idle;
        }

        public override void Initialize(BehaviourFSM behaviourFSM, Transform owner)
        {
            base.Initialize(behaviourFSM, owner);
            _animator = owner.GetComponent<Animator>();
            _speedParamHash = Animator.StringToHash(_speedParamName);
        }

        public override void Start()
        {
            _animator.SetFloat(_speedParamHash, 0f);
        }

        public override void LogicUpdate()
        {
            if (Input.GetKeyDown(_startWalkKey))
            {
                BehaviourFSM.SetState(StateType.FollowPath);
            }
        }
    }
}
