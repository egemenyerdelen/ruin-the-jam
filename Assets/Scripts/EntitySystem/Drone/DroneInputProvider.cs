using Systems.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntitySystem.Drone
{
    public class DroneInputProvider : InputProvider
    {
        public Vector3 FlightInput { get; private set; }
        public Vector2 ThrustInput { get; private set; }
        
        private InputSystem_Actions.DroneActions _droneInputActions;

        private void Start()
        {
            _droneInputActions = InputManager.InputSystem.Drone;
        }

        private void OnDisable()
        {
            DisableInputs();
        }

        public override void EnableInputs()
        {
            _droneInputActions.Flight.performed += OnFlightPerformed;
            _droneInputActions.Flight.canceled += OnFlightCanceled;

            _droneInputActions.Thrust.performed += OnThrustPerformed;
            _droneInputActions.Thrust.canceled += OnThrustCanceled;
        }

        public override void DisableInputs()
        {
            _droneInputActions.Thrust.performed -= OnThrustPerformed;
            _droneInputActions.Thrust.canceled -= OnThrustCanceled;
            _droneInputActions.Flight.performed -= OnFlightPerformed;
            _droneInputActions.Flight.canceled -= OnFlightCanceled;
        }

        private void OnFlightPerformed(InputAction.CallbackContext ctx) => FlightInput = ctx.ReadValue<Vector3>();
        private void OnFlightCanceled(InputAction.CallbackContext ctx) => FlightInput = Vector3.zero;

        private void OnThrustPerformed(InputAction.CallbackContext ctx) => ThrustInput = ctx.ReadValue<Vector2>();
        private void OnThrustCanceled(InputAction.CallbackContext ctx) => ThrustInput = Vector2.zero;

        public float GetRoll()
        {
            return FlightInput.x;
        }

        public float GetPitch()
        {
            return FlightInput.y;
        }

        public float GetYaw()
        {
            return ThrustInput.x;
        }

        public float GetThrottle()
        {
            return ThrustInput.y;
        }
    }
}