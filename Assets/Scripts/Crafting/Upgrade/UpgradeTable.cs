using System;
using EntitySystem;
using InventorySystem;
using InventorySystem.Interfaces;
using TMPro;
using UI;
using UnityEngine;

namespace Crafting.Upgrade
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
