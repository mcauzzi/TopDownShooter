using System.Collections;
using System.Linq;
using System.Threading.Tasks;
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


        private Coroutine lockOnRoutine;
        private bool      _lockedOn = false;
        private Transform _target;

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
                var size = Physics2D.OverlapCircleAll(transform.position, scanRange);
                Debug.DrawLine(transform.position, transform.position + transform.up * scanRange, Color.red, 1f);
                if (size.Length > 0)
                {
                    _target = size.Where(x => x.CompareTag("Enemy"))
                                  .Select(x => x.transform)
                                  .OrderBy(x => Vector2.Distance(transform.position, x.transform.position))
                                  .FirstOrDefault();
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

            transform.Translate(Vector2.up * (Time.deltaTime * speed));
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