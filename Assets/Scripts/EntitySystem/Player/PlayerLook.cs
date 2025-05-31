using UnityEngine;

namespace EntitySystem.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private GameObject playerCameraObject;
        [SerializeField] private PlayerInputProvider playerInputProvider;
        
        private float _xRotation;
        private float _yRotation;
        
        private void Update()
        {
            var lookDirection = playerInputProvider.GetLookDirection();
            RotatePlayer(lookDirection);
        }
        
        private void RotatePlayer(Vector2 lookDirection)
        {
            var mouseX = lookDirection.x * mouseSensitivity * Time.deltaTime;
            var mouseY = lookDirection.y * mouseSensitivity * Time.deltaTime;

            _yRotation += mouseX;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(0, _yRotation, 0f);
            playerCameraObject.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
    }
}