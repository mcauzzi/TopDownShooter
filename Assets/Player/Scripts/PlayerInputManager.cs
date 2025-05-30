using UnityEngine;
using UnityEngine.InputSystem;
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
        private                                 WeaponGroup[] _weaponGroups;
        private                                 int   _selectedWeaponGroup;

        private void Start()
        {
            SpeedVector = new Vector2(horizontalSpeed, verticalSpeed);
            InitBoundaries();
            _weaponGroups      = GetComponentsInChildren<WeaponGroup>();
            Debug.Log($"Loaded {_weaponGroups.Length} weapons");
            _selectedWeaponGroup = 0;
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
        }

        public void OnMove(InputValue value)
        {
            _inputValue = value.Get<Vector2>();
        }

        public void OnAttack(InputValue value)
        {
            if (value.isPressed)
            {
                _weaponGroups[_selectedWeaponGroup].FireStart();
            }
            else
            {
                _weaponGroups[_selectedWeaponGroup].FireStop();
            }
        }

        public void OnNext(InputValue value)
        {
            if(_selectedWeaponGroup+1< _weaponGroups.Length)
            {
                _selectedWeaponGroup++;
            }
            else
            {
                _selectedWeaponGroup = 0;
            }
            Debug.Log("Selected Weapon: " + _selectedWeaponGroup);
        }
        public void OnPrevious(InputValue value)
        {
            if(_selectedWeaponGroup-1 >= 0)
            {
                _selectedWeaponGroup--;
            }
            else
            {
                _selectedWeaponGroup = _weaponGroups.Length - 1;
            }
            Debug.Log("Selected Weapon: " + _selectedWeaponGroup);
        }
    }
}