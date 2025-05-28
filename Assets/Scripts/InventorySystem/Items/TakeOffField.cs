using System;
using CameraSystem;
using InventorySystem.Interfaces;
using Systems.Input;
using UnityEngine;

namespace InventorySystem.Items
{
    public class TakeOffField : MonoBehaviour, IInteractable
    {
        // [SerializeField] private PlayerDrone playerDrone;
        
        public void Interact(IInteractor interactor)
        {
            // var inputSwitcher = InputSwitcher.Instance;
            //
            // switch (inputSwitcher.activeController)
            // {
            //     case ControllerType.Player:
            //         
            //         inputSwitcher.SwitchController(ControllerType.Drone);
            //         CameraSwitcher.Instance.ActivateDroneFirstPersonCam();
            //         break;
            //     
            //     case ControllerType.Drone:
            //         
            //         // UpgradeManager.Instance.dataHolder.inventory.Add(ItemTypes.Scrap, playerDrone.scrapHolding);
            //         // playerDrone.scrapHolding = 0;
            //         inputSwitcher.SwitchController(ControllerType.Player);
            //         CameraSwitcher.Instance.ActivatePlayerCam();
            //         break;
            //     
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }
    }
}