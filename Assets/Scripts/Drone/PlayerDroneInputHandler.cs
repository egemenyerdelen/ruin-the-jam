using UnityEngine;
using UnityEngine.InputSystem;

namespace Drone
{
    public class PlayerDroneInputHandler : MonoBehaviour
    {
        public Vector3 FlightInput { get; private set; }
        public Vector2 ThrustInput { get; private set; }
        
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
            _inputActions.Drone.Thrust.performed -= OnThrustPerformed;
            _inputActions.Drone.Thrust.canceled -= OnThrustCanceled;
            _inputActions.Drone.Flight.performed -= OnFlightPerformed;
            _inputActions.Drone.Flight.canceled -= OnFlightCanceled;

            _inputActions.Drone.Disable();
        }

        private void OnFlightPerformed(InputAction.CallbackContext ctx) => FlightInput = ctx.ReadValue<Vector3>();
        private void OnFlightCanceled(InputAction.CallbackContext ctx) => FlightInput = Vector3.zero;

        private void OnThrustPerformed(InputAction.CallbackContext ctx) => ThrustInput = ctx.ReadValue<Vector2>();
        private void OnThrustCanceled(InputAction.CallbackContext ctx) => ThrustInput = Vector2.zero;
    }
}