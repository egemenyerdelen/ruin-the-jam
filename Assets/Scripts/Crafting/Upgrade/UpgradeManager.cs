using System;
using System.Collections.Generic;
using EntitySystem;
using EntitySystem.Drone;
using Helpers;
using InventorySystem;
using InventorySystem.Interfaces;
using UnityEngine;

namespace Crafting.Upgrade
{
    public class UpgradeManager : Singleton<UpgradeManager>
    {
        public static event Action<DroneUpgradeDefinition> OnDroneUpgraded;
        private readonly Dictionary<DroneUpgradeType, int> _droneUpgradeLevels = new();

        public void ApplyDroneUpgrade(DroneUpgradeDefinition droneUpgrade)
        {
            var playerInventory = EntityManager.GetFirstEntityOfType(EntityType.Player).EntityDataHolder.inventory;
            
            if (!TrySpendScrap(playerInventory, droneUpgrade.cost))
            {
                Debug.LogWarning("Not enough scrap to upgrade.");
                return;
            }

            var droneObject = EntityManager.GetFirstEntityOfType(EntityType.Drone);
            var droneControllerScript = droneObject.GetComponent<DroneController>();
            if (droneControllerScript == null) return;
            
            var settings = droneControllerScript.RuntimeSettings;

            switch (droneUpgrade.type)
            {
                case DroneUpgradeType.Battery:
                    settings.batteryCap += droneUpgrade.amount;
                    break;
                case DroneUpgradeType.Range:
                    settings.rangeLimit += droneUpgrade.amount;
                    break;
                case DroneUpgradeType.ThrustPower:
                    settings.thrustPower += droneUpgrade.amount;
                    break;
                case DroneUpgradeType.Agility:
                    break;
                case DroneUpgradeType.ScrapCapacity:
                    settings.scrapCapacity += droneUpgrade.amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            OnDroneUpgraded?.Invoke(droneUpgrade);

            if (!_droneUpgradeLevels.TryAdd(droneUpgrade.type, 0))
            {
                _droneUpgradeLevels[droneUpgrade.type] += 1;
            }

            Debug.Log($"{droneUpgrade.type}{_droneUpgradeLevels[droneUpgrade.type]}");
        }

        private static bool TrySpendScrap(Inventory inventory, int cost)
        {
            var currentScrap = inventory.Get(ItemTypes.Scrap);

            if (currentScrap < cost) return false;

            inventory.Set(ItemTypes.Scrap, currentScrap - cost);
            return true;
        }
    }
}
