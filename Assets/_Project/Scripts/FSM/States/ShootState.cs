using TestGame.Combat;
using TestGame.Navigation;
using UnityEngine;

namespace TestGame.FSM
{
    [System.Serializable]
    public class ShootState : FSMState
    {
        private Animator _animator;
        private int _combatParamHash;
        private Waypoint _target;
        private float _aimOffset = 10f;

        [SerializeField] private string _combatParamName = "InCombat";
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private Waypath _path;
        [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;

        public override StateType GetStateType()
        {
            return StateType.Shoot;
        }

        public override void Initialize(BehaviourFSM behaviourFSM, Transform owner)
        {
            base.Initialize(behaviourFSM, owner);

            _animator = owner.GetComponent<Animator>();
            _combatParamHash = Animator.StringToHash(_combatParamName);

            if (!_path)
            {
                _path = Object.FindObjectOfType<Waypath>();
            }

            if (!_path)
            {
                Debug.Log("No waypath found on scene!");
            }
        }

        public override void Start()
        {
            _animator.SetBool(_combatParamHash, true);
            _target = _path.GetCurrentWaypoint();
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(_shootKey))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 targetPosition = ray.origin + _aimOffset * ray.direction;
                Vector3 direction = (targetPosition - _bulletSpawnPosition.position).normalized;
                GameObject bulletObject = BulletsPool.Instance.GetBullet();
                bulletObject.transform.position = _bulletSpawnPosition.position;
                bulletObject.transform.forward = direction;
            }
        }

        public override void LogicUpdate()
        {
            if (_target.NoEnemiesLeft())
            {
                _path.MoveNext();
                BehaviourFSM.SetState(StateType.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _animator.SetBool(_combatParamHash, false);
        }
    }
}
