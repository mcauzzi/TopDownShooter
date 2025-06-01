using Shared.Scripts;
using UnityEngine;

namespace ShipParts.Engines
{
    public class EngineController : MonoBehaviour
    {
        [SerializeField] private EngineStats   engineStats;
        private                  Rigidbody2D   _parentBody;
        private                  ConstantSound _sound;
        public                   EngineStatus  Status { get; set; }

        public void Start()
        {
            _parentBody = transform.parent.GetComponent<Rigidbody2D>();
            _sound      = GetComponent<ConstantSound>();
        }

        public void Update()
        {
            if (_sound)
            {
                if (Status != EngineStatus.Idle)
                {
                    _sound.PlaySound();
                }
                else
                {
                    _sound.StopSound();
                }
            }

            ManageEngineSpeed();

            ManageRotation();
        }

        private void ManageRotation()
        {
            if ((Status & EngineStatus.RotatingLeft) == EngineStatus.RotatingLeft)
            {
                _parentBody.angularVelocity = engineStats.RotationSpeed;
            }
            else if ((Status & EngineStatus.RotatingRight) == EngineStatus.RotatingRight)
            {
                _parentBody.angularVelocity = -engineStats.RotationSpeed;
            }
            else
            {
                _parentBody.angularVelocity = 0f;
            }
        }

        private void ManageEngineSpeed()
        {
            if ((Status & EngineStatus.Accelerating) == EngineStatus.Accelerating)
            {
                _parentBody.AddForce(transform.up * (engineStats.Power * Time.deltaTime), ForceMode2D.Force);
                if (_parentBody.linearVelocity.magnitude > engineStats.MaxSpeed)
                {
                    _parentBody.linearVelocity = _parentBody.linearVelocity.normalized * engineStats.MaxSpeed;
                }
            }
            else if ((Status & EngineStatus.Decelerating) == EngineStatus.Decelerating)
            {
                _parentBody.AddForce(-_parentBody.linearVelocity.normalized * (engineStats.Power * Time.deltaTime),
                                     ForceMode2D.Force);
                if (_parentBody.linearVelocity.magnitude < 0.1f)
                {
                    _parentBody.linearVelocity = Vector2.zero;
                    Status                     = EngineStatus.Idle;
                }
            }
        }
    }
}