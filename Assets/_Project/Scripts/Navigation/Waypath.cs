using UnityEngine;

namespace TestGame.Navigation
{
    public class Waypath : MonoBehaviour
    {
        private int _currentWaypointIndex = 1;
        private Waypoint[] _waypoints;

        private void Awake()
        {
            _waypoints = GetComponentsInChildren<Waypoint>();
        }

        public Waypoint GetCurrentWaypoint()
        {
            return _waypoints[_currentWaypointIndex];
        }

        public bool MoveNext()
        {
            if (_currentWaypointIndex == _waypoints.Length - 1)
            {
                return false;
            }

            _currentWaypointIndex++;
            return true;
        }

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private float _debugSphereRadius = 0.5f;
        [SerializeField] private Color _debugSphereColor = Color.red;
        [SerializeField] private Color _debugLineColor = Color.blue;

        private void OnDrawGizmos()
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = _debugSphereColor;

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                var position = transform.GetChild(i).transform.position;
                Gizmos.DrawSphere(position, _debugSphereRadius);
                Gizmos.color = _debugLineColor;
                Gizmos.DrawLine(position, transform.GetChild(i + 1).transform.position);
                Gizmos.color = _debugSphereColor;
            }

            Gizmos.DrawSphere(transform.GetChild(transform.childCount - 1).transform.position, _debugSphereRadius);

            Gizmos.color = oldColor;
        }
#endif
    }
}