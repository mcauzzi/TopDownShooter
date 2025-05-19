using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Scripts
{
    public class Pathfinder : MonoBehaviour
    {
        [field:SerializeField] public WaveConfigSO    WaveConfig { get; set; }
        private                 List<Transform> _waypoints;
        private                 int             _currentWaypointIndex = 0;
        private void Start()
        {
            _waypoints          = WaveConfig.GetWaypoints();
            transform.position = _waypoints[0].position;
        }

        private void Update()
        {
            FollowPath();
        }

        private void FollowPath()
        {
            if (_currentWaypointIndex == _waypoints.Count)
            {
                Destroy(gameObject);
                return;
            }
            var targetPosition = _waypoints[_currentWaypointIndex].position;
            var movement       = WaveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movement);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                _currentWaypointIndex++;
            }
        }
    }
}
