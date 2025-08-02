using _RuinTheJam.Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _RuinTheJam.Systems.EntitySystem.Player
{
    public class PlayerInputProvider : InputProvider
    {
        private InputSystem_Actions.PlayerActions _playerInputActions;
        private Vector2 _moveDirection;
        private Vector2 _lookDirection;

        private void Start()
        {
            _playerInputActions = InputManager.InputSystem.Player;
            
            EnableInputs();
        }

        private void OnDisable()
        {
            DisableInputs();
        }

        public override void EnableInputs()
        {
            _playerInputActions.Move.performed += UpdateMoveDirection;
            _playerInputActions.Move.canceled += ctx => _moveDirection = Vector2.zero;

            _playerInputActions.Look.performed += UpdateLookDirection;
            _playerInputActions.Look.canceled += ctx => _lookDirection = Vector2.zero;
        }

        public override void DisableInputs()
        {
            _playerInputActions.Move.performed -= UpdateMoveDirection;
            _playerInputActions.Move.canceled -= ctx => _moveDirection = Vector2.zero;
            
            _playerInputActions.Look.performed -= UpdateLookDirection;
            _playerInputActions.Look.canceled -= ctx => _lookDirection = Vector2.zero;
        }

        public Vector2 GetMoveDirection()
        {
            return _moveDirection;
        }

        public Vector2 GetLookDirection()
        {
            return _lookDirection;
        }

        private void UpdateMoveDirection(InputAction.CallbackContext context) => _moveDirection = context.ReadValue<Vector2>();
        private void UpdateLookDirection(InputAction.CallbackContext context) => _lookDirection = context.ReadValue<Vector2>();
    }
}