using System;
using UnityEngine;

namespace Systems.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputSystem_Actions InputSystem;

        public static event Action OnInputSystemCreated;
        protected void Awake()
        {
            InputSystem = new InputSystem_Actions();
            OnInputSystemCreated?.Invoke();
        }

        private void OnEnable()
        {
            InputSystem.Enable();
        }

        private void OnDisable()
        {
            InputSystem.Disable();
        }

        public static void EnableInputSystem()
        {
            InputSystem.Enable();
        }

        public static void DisableInputSystem()
        {
            InputSystem.Disable();
        }
    }
}
