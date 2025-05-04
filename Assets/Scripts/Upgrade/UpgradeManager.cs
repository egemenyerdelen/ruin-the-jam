using Helpers;
using InventorySystem;
using Player;
using UnityEngine;

namespace Upgrade
{
    public class UpgradeManager : Singleton<UpgradeManager>
    {
        public EntityDataHolder dataHolder;
    
        [SerializeField] private GameObject drone;
        [SerializeField] private GameObject player;
        [SerializeField] private int carryUpgradeCost;
        [SerializeField] private int batteryUpgradeCost;
        [SerializeField] private int distanceUpgradeCost;
        [SerializeField] private int engineUpgradeCost;
        [SerializeField] private PlayerDrone playerDrone;

        public void BatteryUpgrade()
        {
            Debug.Log("BUTTON WORKED");
            if (!CanUpgrade(batteryUpgradeCost)) return;

            SpendScrap(batteryUpgradeCost);
            playerDrone.batteryCap += 10;
        }

        public void CarryUpgrade()
        {
            if (!CanUpgrade(carryUpgradeCost)) return;
            
            SpendScrap(carryUpgradeCost);
            playerDrone.scrapCapacity++;
        }

        public void DistanceUpgrade()
        {
            if (!CanUpgrade(distanceUpgradeCost)) return;
            
            SpendScrap(distanceUpgradeCost);
            playerDrone.distanceLimit = 100;
        }

        public void EngineUpgrade()
        {
            if (!CanUpgrade(engineUpgradeCost)) return;
            if (playerDrone.maxThrust >= 8) return;
            
            SpendScrap(engineUpgradeCost);
            playerDrone.maxThrust += 2;
        }

        private bool CanUpgrade(int upgradeCost)
        {
            var activeCount = dataHolder.inventory.Get(ItemTypes.Scrap);

            return activeCount > upgradeCost;
        }

        private void SpendScrap(int upgradeCost)
        {
            var activeCount = dataHolder.inventory.Get(ItemTypes.Scrap);
            dataHolder.inventory.Set(ItemTypes.Scrap, activeCount - upgradeCost);
        }
    }
}
