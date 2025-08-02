using UnityEngine;

namespace _RuinTheJam.Systems.EntitySystem.Drone
{
    public class DronePhysics
    {
        public DroneSettings Settings;
        private Rigidbody _body;

        public Vector3 Torque { get; private set; }
        public Vector3 Force { get; private set; }

        public DronePhysics(Rigidbody rb, DroneSettings droneSettings)
        {
            _body = rb;
            Settings = droneSettings;
        }

        public void UpdatePhysics(float roll, float pitch, float yaw, float throttle, float deltaTime)
        {
            ApplyThrust(throttle, deltaTime);
            ApplyTorque(roll, pitch, yaw, deltaTime);
            ApplySmoothing(deltaTime);
            ApplyFlightAssist(roll, pitch, deltaTime);
        }

        private void ApplyThrust(float throttle, float deltaTime)
        {
            Force = _body.transform.up * (Settings.idleThrust + Settings.thrustPower * throttle * deltaTime);
            Force -= _body.linearVelocity * Settings.dragCoefficient;
        }

        private void ApplyTorque(float roll, float pitch, float yaw, float deltaTime)
        {
            Torque = _body.transform.forward * (-roll * Settings.rollRate * deltaTime)
                     + _body.transform.right * (pitch * Settings.pitchRate * deltaTime)
                     + _body.transform.up * (yaw * Settings.yawRate * deltaTime);
        }

        private void ApplySmoothing(float deltaTime)
        {
            Torque += -_body.angularVelocity * (Settings.motionSmoothness * deltaTime);
        }

        private void ApplyFlightAssist(float roll, float pitch, float deltaTime)
        {
            if (Mathf.Abs(roll) > Settings.rollDeadZone || Mathf.Abs(pitch) > Settings.pitchDeadZone) return;

            Vector3 stabilize = Vector3.Cross(_body.transform.up, Vector3.up) * Settings.flightAssist;
            stabilize -= _body.angularVelocity * (Settings.flightAssist * 0.01f);
            Torque += stabilize * 0.05f;
        }
    }

}