using UnityEngine;
using UnityEngine.InputSystem;

namespace Drone
{
    public class DroneInputs : MonoBehaviour
    {
        [SerializeField] public DroneController droneController;
        
        private InputSystem_Actions _inputActions;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Drone.Enable();
            _inputActions.Drone.Flight.performed += OnFlightPerformed;
            _inputActions.Drone.Flight.canceled += OnFlightCanceled;
            _inputActions.Drone.Thrust.performed += OnThrustPerformed;
            _inputActions.Drone.Thrust.canceled += OnThrustCanceled;
        }

        private void OnDisable()
        {
            _inputActions.Drone.Flight.performed -= OnFlightPerformed;
            _inputActions.Drone.Flight.canceled -= OnFlightCanceled;
            _inputActions.Drone.Thrust.performed -= OnThrustPerformed;
            _inputActions.Drone.Thrust.canceled -= OnThrustCanceled;
            _inputActions.Drone.Disable();
        }

        private void OnFlightPerformed(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector3>();
            droneController.SetInputs(input, Vector2.zero);
        }

        private void OnFlightCanceled(InputAction.CallbackContext context)
        {
            droneController.SetInputs(Vector3.zero, Vector2.zero);
        }

        private void OnThrustPerformed(InputAction.CallbackContext context)
        {
            var throttle = context.ReadValue<Vector2>();
            droneController.SetInputs(Vector3.zero, throttle);
        }

        private void OnThrustCanceled(InputAction.CallbackContext context)
        {
            droneController.SetInputs(Vector3.zero, Vector2.zero);
        }
    }
}