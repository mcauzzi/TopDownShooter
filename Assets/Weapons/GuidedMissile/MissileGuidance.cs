using System.Collections;
using Shared.Scripts.IFF;
using ShipParts.Radar;
using UnityEngine;

namespace Weapons.GuidedMissile
{
    public class MissileGuidance : MonoBehaviour
    {
        [SerializeField] private float speed     = 10f;
        [SerializeField] private float turnSpeed = 5f;

        [SerializeField] private float     lifeTime = 5f;
        private                  Radar     _radar;
        private                  Coroutine _radarCheckRoutine;
        private                  Transform _target;
        public                   Iff       Iff        { private get; set; }
        private void Start()
        {
            _radar = GetComponent<Radar>();
            if (!_radar)
            {
                Debug.LogError("MissileGuidance requires a Radar component.");
                return;
            }

            _radar.StartScan();
            _radarCheckRoutine = StartCoroutine(CheckRadarTargets());
        }

        private IEnumerator CheckRadarTargets()
        {
            while (true)
            {
                if (_radar.HasTargets)
                {
                    EvaluateRadarTargets();
                }

                if (!_target && !_radar.RadarEnabled)
                {
                    _radar.StartScan();
                }

                yield return new WaitForSeconds(0.1f); // Adjust the frequency of checks as needed
            }
        }

        private void EvaluateRadarTargets()
        {
            Debug.Log("Radar has targets, evaluating...");
            var       minDistance   = float.MaxValue;
            Transform closestTarget = null;
            foreach (var hit in _radar.Targets)
            {
                var targetIff = hit.Iff;
                if (!Iff.CanTargetIff(targetIff))
                {
                    continue;
                }

                var   missilePos = transform.position;
                float distance   = Vector2.SqrMagnitude(hit.Target.position - missilePos);
                if (distance < minDistance)
                {
                    minDistance   = distance;
                    closestTarget = hit.Target;
                }
            }

            if (closestTarget)
            {
                Debug.Log($"Closest target found: {closestTarget.name} at distance {Mathf.Sqrt(minDistance)}");
                _target = closestTarget;
                _radar.StopScan();
            }
        }

        private void Update()
        {
            if (HasObstacleInFront())
            {
                // Move in a random direction
                var randomDirection = Random.insideUnitCircle.normalized;
                transform.Rotate(Vector3.forward,
                                 Vector2.SignedAngle(Vector2.up, randomDirection) * turnSpeed * Time.deltaTime);
            }
            else if (_target)
            {
                // Move towards the target
                RotateToTarget();
            }

            transform.Translate(transform.up * (Time.deltaTime * speed), Space.World);
            CheckLifeTime();
        }

        private bool HasObstacleInFront()
        {
            var hit = Physics2D.Raycast(transform.position, transform.up, speed * 0.5f, LayerMask.GetMask("Player"));
            if (hit.collider)
            {
                Debug.DrawLine(transform.position, hit.point, Color.green, 1f);

                return true;
            }

            return false;
        }

        private void CheckLifeTime()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void RotateToTarget()
        {
            Vector2 direction = _target.position - transform.position;
            direction.Normalize();
            var   angle          = Vector2.SignedAngle(transform.up, direction);
            float rotationAmount = Mathf.Sign(angle) * turnSpeed * Time.deltaTime;

            // Limita la rotazione alla quantità di angolo rimanente (evita oscillazioni)
            if (Mathf.Abs(rotationAmount) > Mathf.Abs(angle))
                rotationAmount = angle;

            transform.Rotate(Vector3.forward, rotationAmount);
        }
    }
}