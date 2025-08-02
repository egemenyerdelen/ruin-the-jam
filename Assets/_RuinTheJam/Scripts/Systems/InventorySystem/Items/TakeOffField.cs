using System;
using _RuinTheJam.Systems.EntitySystem;
using _RuinTheJam.Systems.InteractionSystem.Interfaces;
using UnityEngine;

namespace _RuinTheJam.Systems.InventorySystem.Items
{
    public class TakeOffField : MonoBehaviour, IInteractable
    {
        public void Interact(IInteractor interactor)
        {
            var controlSwitcher = EntitySwitcher.Instance;
            
            switch (controlSwitcher.activeEntity)
            {
                case EntityType.Player:
                    
                    controlSwitcher.SwitchEntity(EntityType.Drone);
                    break;
                
                case EntityType.Drone:
                    
                    // UpgradeManager.Instance.dataHolder.inventory.Add(ItemTypes.Scrap, playerDrone.scrapHolding);
                    // playerDrone.scrapHolding = 0;
                    controlSwitcher.SwitchEntity(EntityType.Player);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}