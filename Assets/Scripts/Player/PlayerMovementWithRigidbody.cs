using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementWithRigidbody : MonoBehaviour
    {
        public Rigidbody playerRigidbody;
        public float CurrentMoveMagnitude => _moveDirection.magnitude;
        
        [SerializeField] private float playerMovementForce;

        private InputSystem_Actions _inputSystem;
        private Vector2 _moveDirection;

        private void Start()
        {
            CreateAndPrepareInputSystem();
        }

        private void OnDisable()
        {
            if (_inputSystem == null) return;
            
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
            var move = transform.forward * _moveDirection.y + transform.right * _moveDirection.x;
            playerRigidbody.AddForce(move * playerMovementForce);
        }
    }
}