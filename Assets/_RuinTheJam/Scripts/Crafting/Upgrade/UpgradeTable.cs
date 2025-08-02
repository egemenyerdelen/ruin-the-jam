using _RuinTheJam.Systems.EntitySystem;
using _RuinTheJam.Systems.InteractionSystem.Interfaces;
using _RuinTheJam.Systems.InventorySystem;
using _RuinTheJam.UI;
using TMPro;
using UnityEngine;

namespace _RuinTheJam.Crafting.Upgrade
{
    public class UpgradeTable : MonoBehaviour, IInteractable
    {
        [SerializeField] private TextMeshPro scrapText;

        private Inventory _playerInventory;
        
        private void Start()
        {
            _playerInventory = EntityManager.GetFirstEntityOfType(EntityType.Player).Inventory;

            _playerInventory.OnItemAdded += UpdateScrapTextOnItemAmountChanged;
            _playerInventory.OnItemRemoved += UpdateScrapTextOnItemAmountChanged;
        }

        private void OnDisable()
        {
            _playerInventory.OnItemAdded -= UpdateScrapTextOnItemAmountChanged;
            _playerInventory.OnItemRemoved -= UpdateScrapTextOnItemAmountChanged;
        }

        public void Interact(IInteractor interactor)
        {
            UIManager.Instance.OpenUpgradeMenu();
            
            //CameraSwitcher.Instance.SwitchUpgradeCamera();
        }

        private void UpdateScrapTextOnItemAmountChanged(ItemTypes type)
        {
            scrapText.text = $"{_playerInventory.Get(type)}";
        }
    }
}
