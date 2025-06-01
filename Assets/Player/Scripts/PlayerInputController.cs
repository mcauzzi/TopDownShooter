using ShipParts.Engines;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Player.Scripts
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Padding          padding;
        [SerializeField] private EngineStats      engineStats;
        private                  float            _currentSpeed;
        private                  Rigidbody2D      _rigidBody;
        private                  Vector2          _inputValue;
        private                  EngineController _engine;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _engine    = GetComponentInChildren<EngineController>();
        }

        private void Update()
        {
            if (_inputValue.y > 0)
            {
                _engine.Status = EngineStatus.Accelerating;
            }
            else if (_inputValue.y < 0)
            {
                _engine.Status = EngineStatus.Decelerating;
            }
            else
            {
                _engine.Status = EngineStatus.Idle;
            }
            
            if (_inputValue.x > 0)
            {
                _engine.Status |= EngineStatus.RotatingRight;
            }
            else if (_inputValue.x < 0)
            {
                _engine.Status |= EngineStatus.RotatingLeft;
            }
            else
            {
                //Removes the rotation bits associated with rotation from the engine status
                _engine.Status &= ~(EngineStatus.RotatingLeft | EngineStatus.RotatingRight);
            }
        }
        
        //
        // private void Movement()
        // {
        //     if (_inputValue.y > 0 && _currentSpeed < engineStats.MaxSpeed)
        //     {
        //         _currentSpeed += engineStats.Acceleration * Time.deltaTime;
        //     }
        //     else if (_inputValue.y < 0 && _currentSpeed > -engineStats.MaxSpeed)
        //     {
        //         _currentSpeed -= engineStats.Acceleration * Time.deltaTime;
        //     }
        //
        //     _rigidBody.linearVelocity = transform.up * _currentSpeed;
        // }

        public void OnMove(InputValue value)
        {
            _inputValue = value.Get<Vector2>();
        }
    }
}