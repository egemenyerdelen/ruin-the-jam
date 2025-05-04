using System;
using CameraSystem;
using Helpers;

namespace Input
{
    public class InputSwitcher : Singleton<InputSwitcher>
    {
        public ControllerType activeController = ControllerType.Player;
        
        public void SwitchController(ControllerType controllerType)
        {
            activeController = controllerType;
            
            switch (controllerType)
            {
                case ControllerType.Player:
                    InputManager.InputSystem.Drone.Disable();
                    
                    InputManager.InputSystem.Player.Enable();
                    CameraManager.Instance.SwitchPlayerCamera();

                    break;
                case ControllerType.Drone:
                    InputManager.InputSystem.Player.Disable();
                    
                    InputManager.InputSystem.Drone.Enable();
                    CameraManager.Instance.SwitchDroneCamera();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(controllerType), controllerType, null);
            }
        }
    }

    public enum ControllerType
    {
        Player,
        Drone
    }
}