using UnityEngine;

namespace Drone
{
    [CreateAssetMenu(fileName = "DroneSettings", menuName = "Drone/Settings")]
    public class DroneSettings : ScriptableObject
    {
        public float batteryCap = 100;
        public float idleThrust = 10f;
        public float maxThrust = 100f;
        public float rollRate = 50f;
        public float pitchRate = 50f;
        public float yawRate = 50f;
        public float flightAssist = 2f;
        public float rollDeadZone = 2f;
        public float pitchDeadZone = 2f;
        public float dragCoefficient = 0.2f;
        public float limitCoefficient = 1f;
        public float rangeLimit = 30f;
    }

}