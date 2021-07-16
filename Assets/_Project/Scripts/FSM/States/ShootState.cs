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
        private int _shootParamHash;
        private Waypoint _target;
        private float _aimDistance = 100f;

        [SerializeField] private string _combatParamName = "InCombat";
        [SerializeField] private string _shootParamName = "Shot";
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private Waypath _path;
        [SerializeField] private LayerMask _shootLayers;
        [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;
        [SerializeField] private float _aimOffset = 5f;

        public override StateType GetStateType()
        {
            return StateType.Shoot;
        }

        public override void Initialize(BehaviourFSM behaviourFSM, Transform owner)
        {
            base.Initialize(behaviourFSM, owner);

            _animator = owner.GetComponent<Animator>();
            _combatParamHash = Animator.StringToHash(_combatParamName);
            _shootParamHash = Animator.StringToHash(_shootParamName);

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

            if (Input.GetKeyDown(_shootKey) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                Vector3 targetPosition;

                if (Physics.Raycast(ray.origin, ray.direction, out hit, _aimDistance, _shootLayers))
                {
                    targetPosition = hit.point;
                }
                else
                {
                    targetPosition = ray.origin + _aimOffset * ray.direction;
                }

                Vector3 direction = (targetPosition - _bulletSpawnPosition.position).normalized;
                GameObject bulletObject = BulletsPool.Instance.GetBullet();
                bulletObject.transform.position = _bulletSpawnPosition.position;
                bulletObject.transform.forward = direction;
                _animator.SetTrigger(_shootParamHash);
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
