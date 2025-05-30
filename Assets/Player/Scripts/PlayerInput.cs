using System.Collections.Generic;
using SharedScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Weapons;

namespace Player.Scripts
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private float     horizontalSpeed = 20;
        [SerializeField, Range(1, 100)] private float     verticalSpeed   = 40;
        [SerializeField]                private Padding   padding;
        private                                 Vector2   _minBound;
        private                                 Vector2   _maxBound;
        private                                 Vector2   _inputValue;
        private                                 IWeapon[] _fireables;
        private                                 int   _selectedWeaponIndex;

        private void Start()
        {
            SpeedVector = new Vector2(horizontalSpeed, verticalSpeed);
            InitBoundaries();
            _fireables      = GetComponentsInChildren<IWeapon>();
            _selectedWeaponIndex = 0;
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

        public void OnMove(InputValue value)
        {
            _inputValue = value.Get<Vector2>();
        }

        public void OnAttack(InputValue value)
        {
            if (value.isPressed)
            {
                _fireables[_selectedWeaponIndex].FireStart();
            }
            else
            {
                _fireables[_selectedWeaponIndex].FireStop();
            }
        }

        public void OnNext(InputValue value)
        {
            if(_selectedWeaponIndex+1< _fireables.Length)
            {
                _selectedWeaponIndex++;
            }
            else
            {
                _selectedWeaponIndex = 0;
            }
        }
        public void OnPrevious(InputValue value)
        {
            if(_selectedWeaponIndex-1 >= 0)
            {
                _selectedWeaponIndex--;
            }
            else
            {
                _selectedWeaponIndex = _fireables.Length - 1;
            }
            Debug.Log("Selected Weapon: " + _selectedWeaponIndex);
        }
    }
}