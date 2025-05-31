using Sirenix.OdinInspector;
using UnityEngine;

namespace EntitySystem.Drone
{
    public class DroneController : MonoBehaviour
    {
        [Header("References")]
        [Required, SerializeField] private DroneInputProvider inputProvider;
        [Required, SerializeField] private DroneSettings droneSettings;
        [Required, SerializeField] private Rigidbody droneRigidbody;

        public DroneBattery Battery { get; private set; }
        public DronePhysics Physics { get; private set; }
        
        private DroneInput _input;
        private DroneSignal _signal;
        private DroneSettings _runtimeSettings;

        private void Start()
        {
            var player = EntityManager.GetFirstEntityOfType(EntityType.Player);
            _runtimeSettings = Instantiate(droneSettings);
            
            _input = new DroneInput(inputProvider);
            Battery = new DroneBattery(droneSettings.batteryCap, 0.4f);
            Physics = new DronePhysics(droneRigidbody, _runtimeSettings);
            _signal = new DroneSignal(transform, player.transform, droneSettings.rangeLimit, 0.6f);
        }

        private void Update()
        {
            _signal.CheckSignal();
            
            if (Battery.CurrentBattery <= 0 || !_signal.IsInControlRange)
            {
                _input.ResetInputs();
                return;
            }

            var inputLag = _signal.GetCurrentLag();
            _input.SetInputLag(inputLag);

            _input.Read();
            Battery.UpdateBattery(Time.fixedDeltaTime);
        }
        
        private void FixedUpdate()
        {
            Physics.UpdatePhysics(_input.Roll, _input.Pitch, _input.Yaw, _input.Throttle, Time.fixedDeltaTime);
            
            droneRigidbody.AddForce(Physics.Force);
            droneRigidbody.AddTorque(Physics.Torque);
        }
    }
}
