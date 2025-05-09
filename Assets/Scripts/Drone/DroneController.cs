using UnityEngine;

namespace Drone
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroneController : MonoBehaviour
    {
        [SerializeField] private DroneSettings settings;
        private Rigidbody rb;
        private Vector3 inputAxis;
        private Vector2 inputThrottle;
        private float battery;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            battery = settings.batteryCap;
        }

        void Update()
        {
            if (battery <= 0) return;

            HandleInputs();
        }

        void FixedUpdate()
        {
            ApplyPhysics();
        }

        public void SetInputs(Vector3 axis, Vector2 throttle)
        {
            inputAxis = axis;
            inputThrottle = throttle;
        }

        private void HandleInputs()
        {
            if (inputAxis != Vector3.zero || inputThrottle != Vector2.zero)
            {
                battery -= Time.deltaTime * 2f;
            }
        }

        private void ApplyPhysics()
        {
            rb.AddForce(transform.up * (settings.idleThrust + inputThrottle.y * settings.maxThrust));
            rb.AddTorque(CalculateTorque());
            ApplyDrag();
        }
        
        private Vector3 CalculateTorque()
        {
            Vector3 torque = Vector3.zero;
            torque += -inputAxis.x * settings.rollRate * transform.forward;
            torque += inputAxis.y * settings.pitchRate * transform.right;
            torque += inputThrottle.x * settings.yawRate * transform.up;
            return torque;
        }

        private void ApplyDrag()
        {
            rb.linearVelocity *= (1f - settings.dragCoefficient * Time.fixedDeltaTime);
        }
    }
}