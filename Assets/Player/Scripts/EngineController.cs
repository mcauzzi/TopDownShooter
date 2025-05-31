using ShipParts.Engines;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Player.Scripts
{
    public class EngineController : MonoBehaviour
    {
        [SerializeField] private Padding     padding;
        [SerializeField] private EngineStats engineStats;
        private                  float       _currentSpeed;
        private                  Camera      _mainCamera;
        private                  Rigidbody2D _rigidBody;
        private                  Vector2     _inputValue;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rigidBody  = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Rotate();
            Movement();
        }

        private void Rotate()
        {
            if (_inputValue.x < 0)
            { 
                Debug.Log("Rotate Right");
                _rigidBody.MoveRotation(_rigidBody.rotation+engineStats.RotationSpeed * Time.deltaTime);
            }
            else if (_inputValue.x > 0)
            {
                Debug.Log("Rotate left");
                _rigidBody.MoveRotation(_rigidBody.rotation-engineStats.RotationSpeed * Time.deltaTime);
            }
        }

        private void Movement()
        {
            if (_inputValue.y > 0 && _currentSpeed < engineStats.MaxSpeed)
            {
                _currentSpeed += engineStats.Acceleration * Time.deltaTime;
            }
            else if (_inputValue.y < 0 && _currentSpeed > -engineStats.MaxSpeed)
            {
                _currentSpeed -= engineStats.Acceleration * Time.deltaTime;
            }

            _rigidBody.linearVelocity = transform.up * _currentSpeed;
        }

        public void OnMove(InputValue value)
        {
            
            _inputValue = value.Get<Vector2>();
        }
    }
}