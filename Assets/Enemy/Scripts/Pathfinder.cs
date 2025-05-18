using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Scripts
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] private WaveConfigSO      waveConfig;
        private                  List<Transform> waypoints;

        private void Start()
        {
            waypoints          = waveConfig.GetWaypoints();
            transform.position = waypoints[0].position;
        }

        private void Update()
        {
            FollowPath();
        }

        private void FollowPath()
        {
            if (waypoints.Count == 0)
            {
                Destroy(gameObject);
                return;
            }
            var targetPosition = waypoints[0].position;
            var movement       = waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movement);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                waypoints.RemoveAt(0);
            }
        }
    }
}
