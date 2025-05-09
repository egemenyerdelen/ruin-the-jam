using Systems.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private GameObject playerObject;

        private Vector2 _lookDirection;
        private float _xRotation;
        private float _yRotation;

        private InputSystem_Actions _inputSystem;

        private void Start()
        {
            _inputSystem = InputManager.InputSystem;
            _inputSystem.Player.Look.performed += UpdateLookDirection;
        }

        private void Update()
        {
            RotatePlayer();
        }

        private void LateUpdate()
        {
            _lookDirection = Vector2.zero;
        }

        private void OnDisable()
        {
            if (_inputSystem == null) return;
            _inputSystem.Player.Look.performed -= UpdateLookDirection;
        }

        private void UpdateLookDirection(InputAction.CallbackContext context)
        {
            _lookDirection = context.ReadValue<Vector2>();
        }

        private void RotatePlayer()
        {
            var mouseX = _lookDirection.x * mouseSensitivity * Time.deltaTime;
            var mouseY = _lookDirection.y * mouseSensitivity * Time.deltaTime;

            _yRotation += mouseX;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerObject.transform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
        }
    }
}