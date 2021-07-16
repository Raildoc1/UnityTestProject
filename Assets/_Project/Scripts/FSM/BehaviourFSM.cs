using UnityEngine;
using UnityEngine.AI;

namespace TestGame.FSM
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class BehaviourFSM : MonoBehaviour
    {
        private IFSMState _currentState;

        [Header("General")]
        [SerializeField] private Transform _startPoint;

        [Header("States")]
        [SerializeField] private FollowPathState _followPathState;
        [SerializeField] private IdleState _idleState;

        private void Awake()
        {
            transform.position = _startPoint.position;
            transform.rotation = _startPoint.rotation;
            _followPathState.Initialize(this, transform);
            _idleState.Initialize(this, transform);
        }

        private void Start()
        {
            SetState(_idleState);
        }

        private void Update()
        {
            if (_currentState == null)
            {
                return;
            }

            _currentState.Update();
            _currentState.LogicUpdate();
        }

        public void SetState(StateType type)
        {
            switch (type)
            {
                case StateType.Idle:
                    SetState(_idleState);
                    break;
                case StateType.FollowPath:
                    SetState(_followPathState);
                    break;
                case StateType.Shoot:
                    break;
                default:
                    break;
            }
        }

        private void SetState(IFSMState state)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }
            _currentState = state;
            _currentState.Start();
        }
    }
}