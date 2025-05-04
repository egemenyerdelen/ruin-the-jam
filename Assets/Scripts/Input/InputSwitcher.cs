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
                    Player.GetComponent<EntityInteraction>().canInteract = true;
                    
                    CameraManager.Instance.SwitchPlayerCamera();

                    Drone.GetComponent<PlayerDrone>().DisableInputSystem();
                    Drone.GetComponent<EntityInteraction>().canInteract = false;

                    break;
                case ControllerType.Drone:
                    
                    Drone.GetComponent<PlayerDrone>().EnableInputSystem();
                    Drone.GetComponent<PlayerDrone>().battery = Drone.GetComponent<PlayerDrone>().batteryCap;
                    Drone.GetComponent<PlayerDrone>().BatteryHUD[0].gameObject.SetActive(true);
                    Drone.GetComponent<PlayerDrone>().BatteryHUD[1].gameObject.SetActive(true);
                    Drone.GetComponent<PlayerDrone>().BatteryHUD[2].gameObject.SetActive(true);
                    Drone.GetComponent<PlayerDrone>().BatteryHUD[3].gameObject.SetActive(true);
                    Drone.GetComponent<EntityInteraction>().canInteract = true;
                    
                    CameraManager.Instance.SwitchDroneCamera();

                    Player.GetComponent<PlayerMovementWithRigidbody>().DisableInputSystem();
                    Player.GetComponent<EntityInteraction>().canInteract = false;

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