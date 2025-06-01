
using Shared.Scripts;
using Shared.Scripts.IFF;
using ShipParts.Radar;
using UnityEngine;

namespace Enemy.Scripts
{
    public class EnemyBrain : MonoBehaviour
    {
        private Radar     _radar;
        private Transform _target;
        private Iff    _iff;

        private void Start()
        {
            _radar=GetComponent<Radar>();
            _iff=GetComponent<HealthManager>().Iff;
            if (!_radar)
            {
                Debug.LogError("EnemyBrain requires a Radar component.");
                return;
            }
            _radar.RadarTargetsUpdated += EvaluateRadarTargets;
        }

        private void EvaluateRadarTargets(TargetsStruct[] targets)
        {
            var       minDistance   = float.MaxValue;
            Transform closestTarget = null;
            foreach (var hit in targets)
            {
                var targetIff = hit.Iff;
                if (!_iff.CanTargetIff(targetIff))
                {
                    continue;
                }

                var   enemyPos = transform.position;
                float distance = Vector2.SqrMagnitude(hit.Target.position - enemyPos);
                if (distance < minDistance)
                {
                    minDistance   = distance;
                    closestTarget = hit.Target;
                }
            }

            if (closestTarget)
            {
                _target = closestTarget;
                Debug.Log("Found target", _target);
                _radar.StopScan();
            }
        }

        private void Update()
        {
            Move();
            if(!_target && !_radar.IsScanning)
            {
                _radar.StartScan();
            }
        }

        private void Move()
        {
            if (HasObstacleInFront())
            {
                // Move in a random direction
                var randomDirection = Random.insideUnitCircle.normalized;
                transform.Rotate(Vector3.forward,
                                 Vector2.SignedAngle(Vector2.up, randomDirection) * 100 * Time.deltaTime);
            }
            else if (_target)
            {
                RotateToTarget();
                transform.Translate(transform.up * (5 * Time.deltaTime), Space.World);
            }
        }
        private bool HasObstacleInFront()
        {
            var hit = Physics2D.Raycast(transform.position, transform.up, 10 * 0.5f, LayerMask.GetMask("Player"));
            if (hit.collider)
            {
                Debug.DrawLine(transform.position, hit.point, Color.green, 1f);

                return true;
            }

            return false;
        }
        private void RotateToTarget()
        {
            Vector2 direction = _target.position - transform.position;
            direction.Normalize();
            var   angle          = Vector2.SignedAngle(transform.up, direction);
            float rotationAmount = Mathf.Sign(angle) * 100 * Time.deltaTime;

            // Limita la rotazione alla quantità di angolo rimanente (evita oscillazioni)
            if (Mathf.Abs(rotationAmount) > Mathf.Abs(angle))
                rotationAmount = angle;

            transform.Rotate(Vector3.forward, rotationAmount);
        }
    }
}
