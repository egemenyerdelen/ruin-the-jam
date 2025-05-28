using Sirenix.OdinInspector;
using UnityEngine;

namespace EntitySystem.Drone
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroneController : MonoBehaviour
    {
        [Header("References")]
        [Required, SerializeField] private PlayerDroneInputHandler inputProvider;
        [Required, SerializeField] private DroneSettings droneSettings;
        [Required, SerializeField] private Rigidbody droneRigidbody;
        public DroneSettings RuntimeSettings => _runtimeSettings;
        
        private DroneSettings _runtimeSettings;
        
        private Vector3 _force;
        private Vector3 _torque;

        private Vector3 _flightInput;
        private Vector2 _thrustInput;
        private float _roll, _pitch, _yaw, _throttle;

        private void Awake()
        {
            _runtimeSettings = Instantiate(droneSettings);
        }

        private void Update()
        {
            _flightInput = inputProvider.FlightInput;
            _thrustInput = inputProvider.ThrustInput;

            _roll = _flightInput.x;
            _pitch = _flightInput.y;
            _yaw = _thrustInput.x;
            _throttle = _thrustInput.y;
        }

        private void FixedUpdate()
        {
            ApplyDronePhysics();
        }

        private void ApplyDronePhysics()
        {
            _force = transform.up * (_runtimeSettings.idleThrust + _runtimeSettings.thrustPower * _throttle * Time.deltaTime);
            _force -= droneRigidbody.linearVelocity * _runtimeSettings.dragCoefficient;

            var rollVec = transform.forward * (-_roll * _runtimeSettings.rollRate * Time.deltaTime);
            var pitchVec = transform.right * (_pitch * _runtimeSettings.pitchRate * Time.deltaTime);
            var yawVec = transform.up * (_yaw * _runtimeSettings.yawRate * Time.deltaTime);

            _torque = rollVec + pitchVec + yawVec;

            ApplyMotionSmoothing();
            ApplyFlightAssist();

            droneRigidbody.AddForce(_force);
            droneRigidbody.AddTorque(_torque);
        }
        
        private void ApplyMotionSmoothing()
        {
            var assistTorque = Vector3.zero;
            
            assistTorque.x = -droneRigidbody.angularVelocity.x * _runtimeSettings.motionSmoothness * Time.deltaTime;
            
            assistTorque.z = -droneRigidbody.angularVelocity.z * _runtimeSettings.motionSmoothness * Time.deltaTime;

            assistTorque.y = -droneRigidbody.angularVelocity.y * _runtimeSettings.motionSmoothness * Time.deltaTime;

            _torque += assistTorque;
        }

        private void ApplyFlightAssist()
        {
            // Skip assist if the player is actively controlling pitch or roll
            var isInputControlling = 
                Mathf.Abs(_roll) > _runtimeSettings.rollDeadZone || Mathf.Abs(_pitch) > _runtimeSettings.pitchDeadZone;

            if (isInputControlling) return;

            // Align drone's up direction with world up (Vector3.up)
            var currentUp = transform.up;
            var targetUp = Vector3.up;

            // Calculate the torque needed to align up vectors using cross product
            var stabilizationTorque = Vector3.Cross(currentUp, targetUp) * _runtimeSettings.flightAssist;

            // Apply torque smoothing
            // Sorry for magic numbers but its neccessary
            stabilizationTorque -= droneRigidbody.angularVelocity * (_runtimeSettings.flightAssist * 0.01f);

            _torque += stabilizationTorque * .05f;
        }
    }
}
