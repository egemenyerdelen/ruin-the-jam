using System;
using CameraSystem;
using Input;
using UnityEngine;

namespace InventorySystem.Items
{
    public class TakeOffField : HighlightableItem, IInteractable
    {
      
        public void Interact()
        {
            var inputSwitcher = InputSwitcher.Instance;

            switch (inputSwitcher.activeController)
            {
                case ControllerType.Player:
                   
            
                    inputSwitcher.SwitchController(ControllerType.Drone);
                    CameraManager.Instance.SwitchDroneCamera();
                    break;
                
                case ControllerType.Drone:
                    
                
                    inputSwitcher.SwitchController(ControllerType.Player);
                    CameraManager.Instance.SwitchPlayerCamera();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}