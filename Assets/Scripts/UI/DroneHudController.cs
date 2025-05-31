using EntitySystem.Drone;
using InventorySystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DroneHudController : MonoBehaviour
    {
        [Header("Drone")]
        [SerializeField] private DroneEntity droneEntity;
        [SerializeField] private DroneController droneController;
        
        [Header("HUD Elements")]
        public TextMeshProUGUI connectionStatusText;
        public TextMeshProUGUI totalScrap;
        
        [SerializeField] private BatteryIndicator batteryIndicator;

        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
           UnsubscribeFromEvents();
        }

        private void ChangeBatteryIndicatorOnStepChanged(int step)
        {
            if (step == 0)
            {
                var colorChanger = batteryIndicator.indicatorSteps[step].GetComponent<ChangeColorWithTime>();
                if (colorChanger != null)
                {
                    colorChanger.enabled = true;
                }
            }

            // Hide the next step indicator, if it exists
            var nextIndex = step + 1;
            if (nextIndex < batteryIndicator.indicatorSteps.Count)
            {
                batteryIndicator.indicatorSteps[nextIndex].SetActive(false);
            }
        }

        private void ChangeBatteryIndicatorOnDepleted()
        {
            batteryIndicator.indicatorSteps[0].SetActive(false);
        }

        private void OnItemAddedToDroneInventory(ItemTypes itemType, int addedItemCount)
        {
            if (itemType != ItemTypes.Scrap) return;

            var scrapAmount = droneEntity.EntityDataHolder.inventory.Get(ItemTypes.Scrap);
            totalScrap.text = $"Total Scrap: {scrapAmount}";
        }

        private void SubscribeToEvents()
        {
            droneEntity.EntityDataHolder.inventory.OnItemAdded += OnItemAddedToDroneInventory;
            
            droneController.Battery.OnStepChanged += ChangeBatteryIndicatorOnStepChanged;
            droneController.Battery.OnDepleted += ChangeBatteryIndicatorOnDepleted;
        }

        private void UnsubscribeFromEvents()
        {
            droneEntity.EntityDataHolder.inventory.OnItemAdded -= OnItemAddedToDroneInventory;
            
            droneController.Battery.OnStepChanged -= ChangeBatteryIndicatorOnStepChanged;
            droneController.Battery.OnDepleted -= ChangeBatteryIndicatorOnDepleted;
        }
    }
}