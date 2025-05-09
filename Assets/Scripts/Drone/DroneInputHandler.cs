using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Drone
{
    public class DroneInputHandler : MonoBehaviour
    {
        private InputSystem_Actions _inputActions;

        public event Action<Vector3> OnFlightInput;
        public event Action<Vector2> OnThrustInput;
        public event Action OnFlightCanceled;
        public event Action OnThrustCanceled;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Drone.Enable();

            _inputActions.Drone.Flight.performed += HandleFlight;
            _inputActions.Drone.Flight.canceled += HandleFlightCanceled;

            _inputActions.Drone.Thrust.performed += HandleThrust;
            _inputActions.Drone.Thrust.canceled += HandleThrustCanceled;
        }

        private void OnDisable()
        {
            _inputActions.Drone.Flight.performed -= HandleFlight;
            _inputActions.Drone.Flight.canceled -= HandleFlightCanceled;

            _inputActions.Drone.Thrust.performed -= HandleThrust;
            _inputActions.Drone.Thrust.canceled -= HandleThrustCanceled;

            _inputActions.Drone.Disable();
        }

        private void HandleFlight(InputAction.CallbackContext context)
        {
            var flightInput = context.ReadValue<Vector3>();
            OnFlightInput?.Invoke(flightInput);
        }

        private void HandleFlightCanceled(InputAction.CallbackContext context)
        {
            OnFlightCanceled?.Invoke();
        }

        private void HandleThrust(InputAction.CallbackContext context)
        {
            var thrustInput = context.ReadValue<Vector2>();
            OnThrustInput?.Invoke(thrustInput);
        }

        private void HandleThrustCanceled(InputAction.CallbackContext context)
        {
            OnThrustCanceled?.Invoke();
        }
    }
}