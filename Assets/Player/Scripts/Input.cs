using System.Collections.Generic;
using SharedScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player.Scripts
{
    public class Input : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private float       horizontalSpeed = 20;
        [SerializeField, Range(1, 100)] private float       verticalSpeed   = 40;
        [SerializeField]                private Padding     padding;
        private                                 Vector2     _minBound;
        private                                 Vector2     _maxBound;
        private                                 Vector2     _inputValue;
        private                                 IFireable[] _fireables;


        private void Start()
        {
            SpeedVector = new Vector2(horizontalSpeed, verticalSpeed);
            InitBoundaries();
            Debug.Log(padding);
            _fireables = GetComponentsInChildren<IFireable>();
            foreach (var fireable in _fireables)
            {
                Debug.Log("Got fireable", (Object)fireable);
            }
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
            newPos.x           = Mathf.Clamp(newPos.x, _minBound.x + padding.Left,   _maxBound.x - padding.Right);
            newPos.y           = Mathf.Clamp(newPos.y, _minBound.y + padding.Bottom, _maxBound.y - padding.Top);
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

        private void OnAttack(InputValue value)
        {
            foreach (var fireable in _fireables)
            {
                if (value.isPressed)
                {
                    fireable.Fire();
                }
            }
        }
    }
}