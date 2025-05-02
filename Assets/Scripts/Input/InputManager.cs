using System;
using Helpers;

namespace Input
{
    public class InputManager : Singleton<InputManager>
    {
        public static InputSystem_Actions InputSystem;

        public static event Action OnInputSystemCreated;
        protected override void Awake()
        {
            base.Awake();

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
