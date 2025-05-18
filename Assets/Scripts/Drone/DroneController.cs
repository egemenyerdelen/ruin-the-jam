using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Drone
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroneController : MonoBehaviour
    {
        [Header("References")]
        [Required, SerializeField] private PlayerDroneInputHandler inputProvider;
        [Required, SerializeField] private DroneSettings droneSettings;
        [Required, SerializeField] private Rigidbody droneRigidbody;
        
        private Vector3 _force;
        private Vector3 _torque;

        private Vector3 _flightInput;
        private Vector2 _thrustInput;
        private float _roll, _pitch, _yaw, _throttle;

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
            _force = transform.up * (droneSettings.idleThrust + droneSettings.thrustPower * _throttle * Time.deltaTime);
            _force -= droneRigidbody.linearVelocity * droneSettings.dragCoefficient;

            var rollVec = transform.forward * (-_roll * droneSettings.rollRate * Time.deltaTime);
            var pitchVec = transform.right * (_pitch * droneSettings.pitchRate * Time.deltaTime);
            var yawVec = transform.up * (_yaw * droneSettings.yawRate * Time.deltaTime);

            _torque = rollVec + pitchVec + yawVec;

            ApplyMotionSmoothing();
            ApplyFlightAssist();

            droneRigidbody.AddForce(_force);
            droneRigidbody.AddTorque(_torque);
        }
        
        private void ApplyMotionSmoothing()
        {
            var assistTorque = Vector3.zero;
            
            assistTorque.x = -droneRigidbody.angularVelocity.x * droneSettings.motionSmoothness * Time.deltaTime;
            
            assistTorque.z = -droneRigidbody.angularVelocity.z * droneSettings.motionSmoothness * Time.deltaTime;

            assistTorque.y = -droneRigidbody.angularVelocity.y * droneSettings.motionSmoothness * Time.deltaTime;

            _torque += assistTorque;
        }

        private void ApplyFlightAssist()
        {
            // Skip assist if the player is actively controlling pitch or roll
            var isInputControlling = 
                Mathf.Abs(_roll) > droneSettings.rollDeadZone || Mathf.Abs(_pitch) > droneSettings.pitchDeadZone;

            if (isInputControlling) return;

            // Align drone's up direction with world up (Vector3.up)
            var currentUp = transform.up;
            var targetUp = Vector3.up;

            // Calculate the torque needed to align up vectors using cross product
            var stabilizationTorque = Vector3.Cross(currentUp, targetUp) * droneSettings.flightAssist;

            // Apply torque smoothing
            // Sorry for magic numbers but its neccessary
            stabilizationTorque -= droneRigidbody.angularVelocity * (droneSettings.flightAssist * 0.01f);

            _torque += stabilizationTorque * .05f;
        }
    }
}
