using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using SharedScripts;
using SharedScripts.IFF;
using UnityEngine;

namespace Weapons.GuidedMissile
{
    public class MissileGuidance : MonoBehaviour
    {
        [SerializeField] private float speed     = 10f;
        [SerializeField] private float turnSpeed = 5f;

        [SerializeField] private float lifeTime = 5f;

        [Tooltip("Lock on scan interval in milliseconds")] [Header("Scan Settings")] [SerializeField]
        private int scanInterval = 200;

        [SerializeField] private float scanRange = 10f;
        [SerializeField] private float scanAngle = 45f;


        private Coroutine lockOnRoutine;
        private bool      _lockedOn = false;
        private Transform _target;
        public  Iff       Iff { private get; set; }


        private void Start()
        {
            _lockedOn     = false;
            _target       = null;
            lockOnRoutine = StartCoroutine(LockOnTarget());
        }

        private IEnumerator LockOnTarget()
        {
            while (!_lockedOn)
            {
                var possibleTargets = Physics2D.OverlapCircleAll(transform.position, scanRange);
                if (possibleTargets.Length > 0)
                {
                    var closestTarget = GetClosestTarget(possibleTargets);

                    _target = closestTarget;
                    if (_target)
                    {
                        _lockedOn     = true;
                        lockOnRoutine = null;
                        yield break;
                    }
                }

                yield return new WaitForSeconds(scanInterval / 1000f);
            }
        }

        private Transform GetClosestTarget(Collider2D[] possibleTargets)
        {
            Transform closestTarget = null;
            float     minDistance   = float.MaxValue;
            Vector3   missilePos    = transform.position;
            Vector2   missileUp     = transform.up;

            foreach (var hit in possibleTargets)
            {
                var targetIff = hit.GetComponent<HealthManager>()?.Iff ?? Iff.None;
                if (!Iff.CanTargetIff(targetIff))
                {
                    continue;
                }

                Transform targetTransform   = hit.transform;
                Vector2   directionToTarget = (targetTransform.position - missilePos).normalized;

                if (Vector2.Angle(missileUp, directionToTarget) <= scanAngle)
                {
                    float distance = Vector2.SqrMagnitude(targetTransform.position - missilePos);
                    if (distance < minDistance)
                    {
                        minDistance   = distance;
                        closestTarget = targetTransform;
                    }
                }
            }

            return closestTarget;
        }

        private void Update()
        {
            if (!_target)
            {
                RestartTargetScan();
            }

            if (HasObstacleInFront())
            {
                // Move in a random direction
                var randomDirection = Random.insideUnitCircle.normalized;
                transform.Rotate(Vector3.forward,
                                 Vector2.SignedAngle(Vector2.up, randomDirection) * turnSpeed * Time.deltaTime);
            }
            else if (_lockedOn)
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

        private void RestartTargetScan()
        {
            _lockedOn = false;
            if (lockOnRoutine == null)
            {
                lockOnRoutine = StartCoroutine(LockOnTarget());
            }
        }

        private void RotateToTarget()
        {
            Vector2 direction = _target.position - transform.position;
            direction.Normalize();
            var angle = Vector2.SignedAngle(transform.up, direction);
            transform.Rotate(Vector3.forward, angle * turnSpeed * Time.deltaTime);
        }
    }
}