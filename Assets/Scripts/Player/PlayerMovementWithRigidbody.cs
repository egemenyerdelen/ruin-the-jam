using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementWithRigidbody : MonoBehaviour
    {
        public Rigidbody playerRigidbody;
        
        [SerializeField] private float playerMovementForce;

        private InputSystem_Actions _inputSystem;
        private Vector2 _moveDirection; // Save input value continuously

        private void OnEnable()
        {
            CreateAndPrepareInputSystem();
        }

        private void OnDisable()
        {
            if (_inputSystem != null)
                _inputSystem.Player.Move.performed -= UpdateMoveDirection;
        }

        private void CreateAndPrepareInputSystem()
        {
            _inputSystem = InputManager.InputSystem;
            _inputSystem.Player.Enable();
            
            _inputSystem.Player.Move.performed += UpdateMoveDirection;
            _inputSystem.Player.Move.canceled += ctx => _moveDirection = Vector2.zero;
        }

        private void UpdateMoveDirection(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            var force = new Vector3(_moveDirection.x, 0, _moveDirection.y) * playerMovementForce;
            playerRigidbody.AddForce(force);
        }
    }
}