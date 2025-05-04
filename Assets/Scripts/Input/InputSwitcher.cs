using System;
using CameraSystem;
using Helpers;
using Player;
using UnityEngine;

namespace Input
{
    public class InputSwitcher : Singleton<InputSwitcher>
    {
        public ControllerType activeController = ControllerType.Player;

        public GameObject Drone;
        public GameObject Player;
        
        public void SwitchController(ControllerType controllerType)
        {
            activeController = controllerType;
            
            switch (controllerType)
            {
                case ControllerType.Player:
                    
                    Player.GetComponent<PlayerMovementWithRigidbody>().EnableInputSystem();
                    CameraManager.Instance.SwitchPlayerCamera();

                    Drone.GetComponent<PlayerDrone>().DisableInputSystem();

                    break;
                case ControllerType.Drone:
                    
                    Drone.GetComponent<PlayerDrone>().EnableInputSystem();
                    CameraManager.Instance.SwitchDroneCamera();

                    Player.GetComponent<PlayerMovementWithRigidbody>().DisableInputSystem();

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