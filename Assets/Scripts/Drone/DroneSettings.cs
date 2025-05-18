using UnityEngine;

namespace Drone
{
    [CreateAssetMenu(fileName = "DroneSettings", menuName = "Drone/Settings")]
    public class DroneSettings : ScriptableObject
    {
        [Header("Battery")]
        public float batteryCap = 100;
        
        [Header("Range")]
        public float rangeLimit = 100f;
        
        [Header("Thrust")]
        public float idleThrust = 9.8f;
        public float thrustPower = 100f;
        
        [Header("Rotation Rates")]
        public float rollRate = 10f;
        public float pitchRate = 10f;
        public float yawRate = 10f;
        
        [Header("Flight Assist")]
        public float flightAssist = 7f;
        public float motionSmoothness = 5f;
        public float rollDeadZone = .95f;
        public float pitchDeadZone = .95f;
        
        [Header("Physics")]
        public float dragCoefficient = 0.2f;
        public float limitCoefficient = 1f;
    }

}