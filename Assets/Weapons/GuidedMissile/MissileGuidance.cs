using System;
using System.Collections;
using Shared.Scripts;
using Shared.Scripts.IFF;
using ShipParts.Radar;
using UnityEngine;
using Weapons.Interfaces;
using Random = UnityEngine.Random;

namespace Weapons.GuidedMissile
{
    public class MissileGuidance : MonoBehaviour, IBullet
    {
        [SerializeField] private float speed     = 10f;
        [SerializeField] private float turnSpeed = 5f;
        
        [SerializeField] private float     lifeTime = 5f;
        private                  Transform _target;
        public                   Iff       Iff { private get; set; }
        private                  Radar     _radar;
        public                   float     Range { get; private set; }

        private void Start()
        {
            _radar                     =  GetComponent<Radar>();
            _radar.RadarTargetsUpdated += EvaluateRadarTargets;
            if (!_radar)
            {
                Debug.LogError("MissileGuidance requires a Radar component.");
                return;
            }

            _radar.StartScan();
            Range = speed * lifeTime;
        }


        private void EvaluateRadarTargets(TargetsStruct[] targets)
        {
            var       minDistance   = float.MaxValue;
            Transform closestTarget = null;
            foreach (var hit in targets)
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

            if (!_radar.IsScanning && !_target)
            {
                _radar.StartScan();
            }

            transform.Translate(transform.up * (Time.deltaTime * speed), Space.World);
            CheckLifeTime();
        }

        private void OnDestroy()
        {
            if (_radar)
            {
                _radar.RadarTargetsUpdated -= EvaluateRadarTargets;
            }
        }

        private bool HasObstacleInFront()
        {
            var hit = Physics2D.Raycast(transform.position, transform.up, speed * 0.5f);
            if (hit.collider && hit.collider.gameObject!=gameObject && !Iff.CanTargetIff(hit.collider.GetComponent<HealthManager>()?.Iff))
            {
                Debug.Log($"Obstacle detected in front: {hit.collider.name}");
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