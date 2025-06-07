using System;
using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EntitySystem.Drone
{
    public class DroneController : MonoBehaviour
    {
        [Header("References")]
        [Required, SerializeField] private DroneEntity drone;
        [Required, SerializeField] private DroneInputProvider inputProvider;
        [Required, SerializeField] private DroneSettings droneSettings;
        [Required, SerializeField] private Rigidbody droneRigidbody;
        
        // FOR TEST
        [SerializeField] private List<Transform> droneLandingTransforms;

        public DroneBattery Battery { get; private set; }
        public DronePhysics Physics { get; private set; }
        
        private DroneInput _input;
        private DroneSignal _signal;
        private DroneSettings _runtimeSettings;
        private DroneAI _droneAI;

        private void Start()
        {
            var player = EntityManager.GetFirstEntityOfType(EntityType.Player);
            var reversedTransforms = droneLandingTransforms.AsEnumerable()!.Reverse().ToList();
            _runtimeSettings = Instantiate(droneSettings);
            
            _input = new DroneInput(inputProvider);
            Battery = new DroneBattery(droneSettings.batteryCap, 0.4f);
            Physics = new DronePhysics(droneRigidbody, _runtimeSettings);
            _signal = new DroneSignal(transform, player.transform, droneSettings.rangeLimit, 0.6f);
            _droneAI = new DroneAI(this, reversedTransforms);

            _droneAI.OnDroneLanded += OnDroneLanded;
        }

        private void OnDisable()
        {
            _droneAI.OnDroneLanded -= OnDroneLanded;
        }

        private void Update()
        {
            _signal.CheckSignal();

            if (_input.Input.DroneInputActions.Autopilot.WasPerformedThisFrame())
            {
                // TODO: Add DroneAI here for to let AI control drone to home
                EntitySwitcher.Instance.SwitchEntity(EntityType.Player);
                _droneAI.InitializeLandingSequence();
            }
            
            if (Battery.CurrentBattery <= 0 || !_signal.IsInControlRange)
            {
                _input.ResetInputs();
                return;
            }

            var inputLag = _signal.GetCurrentLag();
            _input.SetInputLag(inputLag);

            _input.Read();
            
            UpdateBatteryBasedOnInput();
        }
        
        private void FixedUpdate()
        {
            Physics.UpdatePhysics(_input.Roll, _input.Pitch, _input.Yaw, _input.Throttle, Time.fixedDeltaTime);
            
            droneRigidbody.AddForce(Physics.Force);
            droneRigidbody.AddTorque(Physics.Torque);
        }

        private void UpdateBatteryBasedOnInput()
        {
            var isTakingInput = _input.IsDroneTakingInput();

            if (_droneAI.IsDroneLanded && !isTakingInput)
                return;

            if (isTakingInput)
            {
                _droneAI.IsDroneLanded = false;
                Battery.UpdateBattery(Time.deltaTime);
            }
            else
            {
                Battery.UpdateBattery(Time.deltaTime * 0.2f);
            }

        }
        
        private void OnDroneLanded()
        {
            TransferInventory();
            Battery.RechargeBattery();
        }
        
        private void TransferInventory()
        {
            EntityManager.GetFirstEntityOfType(EntityType.Player).Inventory.Add(ItemTypes.Scrap, drone.Inventory.Get(ItemTypes.Scrap));
        }
    }
}
