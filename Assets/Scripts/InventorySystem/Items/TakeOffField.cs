using System;
using CameraSystem;
using Input;
using UnityEngine;

namespace InventorySystem.Items
{
    public class TakeOffField : HighlightableItem, IInteractable
    {
        public PlayerDrone playerDrone;
        public void Interact()
        {
            var inputSwitcher = InputSwitcher.Instance;

            Debug.Log(inputSwitcher);
            Debug.Log(inputSwitcher.activeController);
            switch (inputSwitcher.activeController)
            {
                case ControllerType.Player:
                    playerDrone.enabled = false;
            
                    inputSwitcher.SwitchController(ControllerType.Drone);
                    CameraManager.Instance.SwitchDroneCamera();
                    break;
                case ControllerType.Drone:
                    playerDrone.enabled = true;
                
                    inputSwitcher.SwitchController(ControllerType.Player);
                    CameraManager.Instance.SwitchPlayerCamera();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}