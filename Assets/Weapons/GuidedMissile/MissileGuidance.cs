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


        private Coroutine      lockOnRoutine;
        private bool      _lockedOn = false;
        private Transform _target;

        private void Start()
        {
            _lockedOn  = false;
            _target    = null;
            lockOnRoutine = StartCoroutine(LockOnTarget());
        }

        private IEnumerator LockOnTarget()
        {
            while (!_lockedOn)
            {
                var size = Physics2D.OverlapCircleAll(transform.position, scanRange);
                Debug.DrawLine(transform.position, transform.position + Vector3.up * scanRange, Color.red, 1f);
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
                _lockedOn = false;
                if (lockOnRoutine == null)
                {
                    lockOnRoutine = StartCoroutine(LockOnTarget());
                }
            }
            if (_lockedOn)
            {
                // Move towards the target
                Vector2 direction = _target.position - transform.position;

                direction.Normalize();
                float angle = Vector2.SignedAngle(transform.up, direction);
                transform.Rotate(Vector3.forward, angle * turnSpeed * Time.deltaTime);
                transform.Translate(Vector2.up * (speed * Time.deltaTime));
            }
            else
            {
                //move forwards
                transform.Translate(Vector2.up * (Time.deltaTime * speed));
            }

            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}