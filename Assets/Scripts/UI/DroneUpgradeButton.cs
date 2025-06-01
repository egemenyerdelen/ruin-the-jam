 using Crafting.Upgrade;
 using TMPro;
 using UnityEngine;
 using UnityEngine.UI;
 
namespace UI
{
    public class DroneUpgradeButton : MonoBehaviour
    {
        [SerializeField] private DroneUpgradeDefinition upgradeDefinition;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TextMeshProUGUI upgradeText;

        private void OnEnable()
        {
            upgradeButton.onClick.AddListener(OnUpgradeClicked);
            
            SetUpgradeData();
        }

        private void OnUpgradeClicked()
        {
            UpgradeManager.Instance.ApplyDroneUpgrade(upgradeDefinition);
            Debug.Log($"UPGRADE DONE {upgradeDefinition.type}");
        }

        private void SetUpgradeData()
        {
            upgradeText.text = name = $"{upgradeDefinition.type}";
        }
    }

}