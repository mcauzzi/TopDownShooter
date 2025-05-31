using ShipParts.Engines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Scripts
{
    public class EngineController : MonoBehaviour
    {
        [SerializeField] private Padding     padding;
        [SerializeField] private EngineStats engineStats;
        private                  float       _currentSpeed;
        private                  bool        _forwardPressed;
        private                  bool        _backwardPressed;
        private                  Camera      _mainCamera;
        private                  Rigidbody2D _rigidBody;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            RotateTowardsMouse();
            Movement();
        }

        private void RotateTowardsMouse()
        {
            // Ottieni la posizione del mouse nello spazio di gioco
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0f; // Assicura che la z sia 0 nel caso di gioco 2D
            
            // Calcola la direzione verso il mouse
            Vector3 direction = mousePosition - transform.position;
            
            // Calcola l'angolo di rotazione
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            // Crea la rotazione target
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            // Applica la rotazione con velocità controllata
            transform.rotation = Quaternion.Slerp(
                                                  transform.rotation,
                                                  targetRotation,
                                                  engineStats.RotationSpeed * Time.deltaTime
                                                 );
        }

        private void Movement()
        {
            
            if (_forwardPressed && _currentSpeed < engineStats.MaxSpeed)
            {
                _currentSpeed+= engineStats.Acceleration * Time.deltaTime;
            }
            else if (_backwardPressed && _currentSpeed>-engineStats.MaxSpeed)
            {
                _currentSpeed-= engineStats.Acceleration * Time.deltaTime;
            }
            _rigidBody.linearVelocity= transform.up * _currentSpeed;
        }

        public void OnForward(InputValue value)
        {
            _forwardPressed = value.isPressed;
        }
        
        public void OnBackward(InputValue value)
        {
            _backwardPressed = value.isPressed;
        }
    }
}