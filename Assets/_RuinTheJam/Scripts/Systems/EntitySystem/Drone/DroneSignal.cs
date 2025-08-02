using _RuinTheJam.UI;
using UnityEngine;

namespace _RuinTheJam.Systems.EntitySystem.Drone
{
    public class DroneSignal
    {
        public bool IsInControlRange { get; private set; } = true;
        
        private Transform _drone;
        private Transform _player;
        private float _rangeLimit;
        private float _weakThreshold;


        public DroneSignal(Transform drone, Transform player, float rangeLimit, float threshold = 0.85f)
        {
            _drone = drone;
            _player = player;
            _rangeLimit = rangeLimit;
            _weakThreshold = threshold;
        }

        public void CheckSignal()
        {
            var distance = Vector3.Distance(_drone.position, _player.position);
            var ratio = distance / _rangeLimit;

            // Signal lost
            if (ratio >= 1f)
            {
                if (IsInControlRange)
                {
                    Debug.Log("Signal lost.");
                    UIManager.Instance.droneHudController.connectionStatusText.text = "Signal lost";
                }

                IsInControlRange = false;
                return;
            }

            // Signal regained
            if (!IsInControlRange)
            {
                Debug.Log("Signal reconnected.");
                UIManager.Instance.droneHudController.connectionStatusText.text = "Strong connection!";
            }

            IsInControlRange = true;

            // Weak signal (but still in range)
            if (ratio >= _weakThreshold)
            {
                Debug.Log("Signal weak!");
                UIManager.Instance.droneHudController.connectionStatusText.text = "Signal weak!";
            }
            else
            {
                // Strong signal
                UIManager.Instance.droneHudController.connectionStatusText.text = "Strong connection!";
            }
        }
        
        public float GetCurrentLag()
        {
            var distance = Vector3.Distance(_drone.position, _player.position);
            var ratio = distance / _rangeLimit;

            if (ratio >= 1) return 1000f;
            
            if (ratio >= _weakThreshold)
                return 0.2f; // weak signal = noticeable delay

            return 0f; // strong signal = no delay
        }
    }
}