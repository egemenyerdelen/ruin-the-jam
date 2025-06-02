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

        private Inventory _droneInventory;

        private void OnEnable()
        {
            ForceBatteryIndicatorUpdate();
        }

        private void Start()
        {
            _droneInventory = droneEntity.Inventory;
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
        
        private void ForceBatteryIndicatorUpdate()
        {
            var step = Mathf.Clamp(Mathf.FloorToInt((droneController.Battery.CurrentBattery / droneController.Battery.MaxBattery) * 4), 0, 4);

            // Turn off all indicators first
            for (var i = 0; i < batteryIndicator.indicatorSteps.Count; i++)
            {
                batteryIndicator.indicatorSteps[i].SetActive(i <= step);
        
                var colorChanger = batteryIndicator.indicatorSteps[i].GetComponent<ChangeColorWithTime>();
                if (colorChanger != null)
                {
                    colorChanger.enabled = (i == 0 && step == 0); // Enable blinking only if we're at the lowest battery
                }
            }

            // Optionally handle depletion state
            if (droneController.Battery.CurrentBattery <= 0)
            {
                batteryIndicator.indicatorSteps[0].SetActive(false);
            }
        }


        private void OnItemAddedToDroneInventory(ItemTypes itemType, int addedItemCount)
        {
            if (itemType != ItemTypes.Scrap) return;
            
            var scrapAmount = _droneInventory.Get(ItemTypes.Scrap);
            totalScrap.text = $"Total Scrap: {scrapAmount}";
        }

        private void SubscribeToEvents()
        {
            _droneInventory.OnItemAdded += OnItemAddedToDroneInventory;
            
            droneController.Battery.OnStepChanged += ChangeBatteryIndicatorOnStepChanged;
            droneController.Battery.OnDepleted += ChangeBatteryIndicatorOnDepleted;
        }

        private void UnsubscribeFromEvents()
        {
            _droneInventory.OnItemAdded -= OnItemAddedToDroneInventory;
            
            droneController.Battery.OnStepChanged -= ChangeBatteryIndicatorOnStepChanged;
            droneController.Battery.OnDepleted -= ChangeBatteryIndicatorOnDepleted;
        }
    }
}