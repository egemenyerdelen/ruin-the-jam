using System;
using CameraSystem;
using Input;
using UnityEngine;
using Upgrade;

namespace InventorySystem.Items
{
    public class TakeOffField : HighlightableItem, IInteractable
    {
        [SerializeField] private PlayerDrone playerDrone;
        
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
                    
                    // UpgradeManager.Instance.dataHolder.inventory.Add(ItemTypes.Scrap, playerDrone.scrapHolding);
                    // playerDrone.scrapHolding = 0;
                    inputSwitcher.SwitchController(ControllerType.Player);
                    CameraManager.Instance.SwitchPlayerCamera();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}