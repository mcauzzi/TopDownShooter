using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Scripts
{
    public class Input : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private float   _horizontalSpeed = 20;
        [SerializeField, Range(1, 100)] private float   _verticalSpeed   = 40;
        [SerializeField]                private Padding padding;
        private                                 Vector2 _minBound;
        private                                 Vector2 _maxBound;
        private                                 Vector2 _inputValue;
        

        private void Start()
        {
            SpeedVector = new Vector2(_horizontalSpeed, _verticalSpeed);
            InitBoundaries();
            Debug.Log(padding);
        }

        private Vector2 SpeedVector { get; set; }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            var delta  = SpeedVector * _inputValue * Time.deltaTime;
            var newPos = new Vector2(transform.position.x, transform.position.y) + delta;
            newPos.x           = Mathf.Clamp(newPos.x, _minBound.x+padding.Left, _maxBound.x-padding.Right);
            newPos.y           = Mathf.Clamp(newPos.y, _minBound.y+padding.Bottom, _maxBound.y-padding.Top);
            transform.position = newPos;
        }

        private void InitBoundaries()
        {
            _minBound = Camera.main.ViewportToWorldPoint(Vector2.zero);
            _maxBound = Camera.main.ViewportToWorldPoint(Vector2.one);
            Debug.Log(_minBound);
            Debug.Log(_maxBound);
        }

        private void OnMove(InputValue value)
        {
            Debug.Log("Input value: " + value.Get<Vector2>());
            _inputValue = value.Get<Vector2>();
        }
    }
}