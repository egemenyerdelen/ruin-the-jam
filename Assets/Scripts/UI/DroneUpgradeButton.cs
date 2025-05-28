 using Crafting.Upgrade;
 using UnityEngine;
 using UnityEngine.UI;
 
namespace UI
{
    public class DroneUpgradeButton : MonoBehaviour
    {
        [SerializeField] private DroneUpgradeDefinition upgradeDefinition;
        [SerializeField] private Button upgradeButton;

        private void Awake()
        {
            upgradeButton.onClick.AddListener(OnUpgradeClicked);
        }

        private void OnUpgradeClicked()
        {
            UpgradeManager.Instance.ApplyDroneUpgrade(upgradeDefinition);
            Debug.Log($"UPGRADE DONE {upgradeDefinition.type}");
        }

        public void SetUpgradeData(DroneUpgradeDefinition data)
        {
            upgradeDefinition = data;
            // optionally update visuals like text, icon, etc
        }
    }

}