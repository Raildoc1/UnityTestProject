using System.Collections;
using TestGame.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace TestGame.FSM
{
    [System.Serializable]
    public class FollowPathState : FSMState
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        private Transform _target;
        private int _speedParamHash;
        private bool _finishedMove = false;
        private bool _finishedRotate = false;

        [SerializeField] private string _speedParamName = "Speed";
        [SerializeField] private Waypath _path;
        [SerializeField] private float _stopDistance = 0.1f;
        [SerializeField] private float _stopAngle = 1f;
        [SerializeField] private float _rotationSpeed = 90f;

        public override StateType GetStateType()
        {
            return StateType.FollowPath;
        }

        public override void Initialize(BehaviourFSM behaviourFSM, Transform owner)
        {
            base.Initialize(behaviourFSM, owner);

            if (!_path)
            {
                _path = Object.FindObjectOfType<Waypath>();
            }

            if (!_path)
            {
                Debug.Log("No waypath found on scene!");
            }

            _agent = Owner.GetComponent<NavMeshAgent>();

            _animator = owner.GetComponent<Animator>();
            _speedParamHash = Animator.StringToHash(_speedParamName);
        }

        public override void Start()
        {
            _target = _path.GetCurrentWaypoint();
            _agent.destination = _target.position;
            _agent.isStopped = false;
        }

        public override void Update()
        {
            if (_finishedMove)
            {
                return;
            }

            float normalizedSpeed = _agent.velocity.magnitude / _agent.speed;
            _animator.SetFloat(_speedParamHash, normalizedSpeed);

            if (_agent.remainingDistance < _stopDistance)
            {
                _finishedMove = true;
                _animator.SetFloat(_speedParamHash, 0f);
                BehaviourFSM.StartCoroutine(RotateRoutine(_target.rotation));
            }
        }

        public IEnumerator RotateRoutine(Quaternion targetRotation)
        {
            while (Quaternion.Angle(targetRotation, Owner.rotation) > _stopAngle)
            {
                Owner.rotation = Quaternion.RotateTowards(Owner.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
            _finishedRotate = true;
        }

        public override void LogicUpdate()
        {
            if (_finishedRotate)
            {
                BehaviourFSM.SetState(StateType.Shoot);
            }
        }

        public override void Exit()
        {
            _agent.isStopped = true;
        }
    }
}
