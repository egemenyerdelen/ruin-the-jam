using System.Collections.Generic;
using UnityEngine;

namespace EntitySystem.Drone
{
    public class DroneInput
    {
        public float Roll { get; private set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }
        public float Throttle { get; private set; }

        private readonly DroneInputProvider _input;
        private readonly Queue<BufferedInput> _inputBuffer = new();
        private float _lagSeconds = 0f;

        public DroneInput(DroneInputProvider inputProvider)
        {
            _input = inputProvider;
        }
        
        public void SetInputLag(float seconds)
        {
            _lagSeconds = Mathf.Max(0f, seconds);
        }

        public void Read()
        {
            var buffered = new BufferedInput
            {
                Timestamp = Time.time + _lagSeconds,
                Roll = _input.GetRoll(),
                Pitch = _input.GetPitch(),
                Yaw = _input.GetYaw(),
                Throttle = _input.GetThrottle()
            };

            _inputBuffer.Enqueue(buffered);

            // Apply input if lag time has passed
            while (_inputBuffer.Count > 0 && _inputBuffer.Peek().Timestamp <= Time.time)
            {
                var input = _inputBuffer.Dequeue();
                Roll = input.Roll;
                Pitch = input.Pitch;
                Yaw = input.Yaw;
                Throttle = input.Throttle;
            }
        }

        public void ResetInputs()
        {
            _inputBuffer.Clear();
            Roll = Pitch = Yaw = Throttle = 0f;
        }
        
        private struct BufferedInput
        {
            public float Timestamp;
            public float Roll;
            public float Pitch;
            public float Yaw;
            public float Throttle;
        }
    }
}