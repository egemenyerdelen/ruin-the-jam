using System;
using UnityEngine;

namespace EntitySystem.Drone
{
    public class DroneBattery
    {
        public float CurrentBattery { get; private set; }
        public float MaxBattery { get; private set; }

        public event Action<int> OnStepChanged;
        public event Action OnDepleted;

        private int _lastStep;
        private float _drainRate;
        private bool _isDepleted = false;
        private int _stepCount;

        public DroneBattery(float maxBattery, float drainRate, int stepCount = 4)
        {
            MaxBattery = maxBattery;
            CurrentBattery = maxBattery;
            _drainRate = drainRate;
            _stepCount = stepCount;
        }

        public void UpdateBattery(float delta)
        {
            if (_isDepleted) return;

            CurrentBattery -= delta * _drainRate;

            var step = Mathf.Clamp(Mathf.FloorToInt((CurrentBattery / MaxBattery) * _stepCount), 0, _stepCount);

            if (CurrentBattery > 0 && step != _lastStep)
            {
                _lastStep = step;
                OnStepChanged?.Invoke(step);
            }

            if (!_isDepleted && CurrentBattery <= 0)
            {
                _isDepleted = true;
                CurrentBattery = 0;
                OnDepleted?.Invoke();
            }
        }
    }
}