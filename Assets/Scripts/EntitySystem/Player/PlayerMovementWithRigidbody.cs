using Systems.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntitySystem.Player
{
    public class PlayerMovementWithRigidbody : MonoBehaviour
    {
        public Rigidbody playerRigidbody;
        public float CurrentMoveMagnitude => _moveDirection.magnitude;
        
        [SerializeField] private float playerMovementForce;
        [SerializeField] private PlayerInputProvider playerInputProvider;
        private Vector2 _moveDirection;

        private void FixedUpdate()
        {
            _moveDirection = playerInputProvider.GetMoveDirection();
            
            var move = transform.forward * _moveDirection.y + transform.right * _moveDirection.x;
            playerRigidbody.AddForce(move * playerMovementForce);
        }
    }
}