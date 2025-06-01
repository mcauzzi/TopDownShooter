using System;
using System.Collections;
using System.Linq;
using Shared.Scripts;
using Shared.Scripts.IFF;
using UnityEngine;

namespace ShipParts.Radar
{
    public class Radar : MonoBehaviour
    {
        [Tooltip("Lock on scan interval in milliseconds"), Header("Scan Settings"), SerializeField]
        private int scanInterval = 200;

        [SerializeField] private float scanRange = 10f;
        [SerializeField] private float scanAngle = 45f;

        private Coroutine       scanRoutine;
        private TargetsStruct[] _targets;

        public  TargetsStruct[] Targets    => _targets;
        public  bool            HasTargets => _targets is { Length: > 0 };
        public bool RadarEnabled => scanRoutine != null;

        private void Start()
        {
            _targets    = Array.Empty<TargetsStruct>();
        }

        public void StartScan()
        {
           
            if (scanRoutine == null)
            {
                scanRoutine = StartCoroutine(ScanArea());
            }
        }

        public void StopScan()
        {
            Debug.Log($"Stopping radar scan.");
            if (scanRoutine != null)
            {
                StopCoroutine(scanRoutine);
                scanRoutine = null;
            }

            _targets    = null;
        }
        
        private IEnumerator ScanArea()
        {
            // Wait for the end of the frame to ensure that the routine is assigned
            yield return new WaitForEndOfFrame();
            while (scanRoutine!=null)
            {
                var possibleTargets = Physics2D.OverlapCircleAll(transform.position, scanRange);
                if (possibleTargets.Length > 0)
                {
                    SetTargets(possibleTargets);
                }
                yield return new WaitForSeconds(scanInterval / 1000f);
            }
        }
        private void SetTargets(Collider2D[] possibleTargets)
        {
            var   missilePos    = transform.position;
            _targets = possibleTargets
                       .Where(x=>IsTargetInScanAngle(x.transform.position - missilePos))
                       .Select(x => CreateTargetStruct(x, missilePos))
                       .ToArray();
        }
        private bool IsTargetInScanAngle(Vector2 directionToTarget)
        {
            // Calcola l'angolo tra la direzione di forward del radar e la direzione verso il target
            var angle = Vector2.Angle(transform.up, directionToTarget);
    
            // Controlla se l'angolo è all'interno del cono di scansione (metà dell'angolo su ogni lato)
            return angle <= scanAngle / 2f;
        }
        private static TargetsStruct CreateTargetStruct(Collider2D x, Vector3 missilePos)
        {
            return new TargetsStruct(x.transform,
                                     Vector2.SqrMagnitude(x.transform.position - missilePos),
                                     (x.transform.position - missilePos).normalized,
                                     x.GetComponent<HealthManager>()?.Iff ?? Iff.None);
        }
    }
}