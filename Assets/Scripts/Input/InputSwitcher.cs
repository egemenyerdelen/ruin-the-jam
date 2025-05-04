using System;
using CameraSystem;
using Helpers;
using UnityEngine;

namespace Input
{
    public class InputSwitcher : Singleton<InputSwitcher>
    {
        public ControllerType activeController = ControllerType.Player;
        
        public void SwitchController(ControllerType controllerType)
        {
            activeController = controllerType;
            InputManager.DisableInputSystem();
            
            switch (controllerType)
            {
                case ControllerType.Player:
                    
                    InputManager.InputSystem.Player.Enable();
                    CameraManager.Instance.SwitchPlayerCamera();

                    break;
                case ControllerType.Drone:
                    
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