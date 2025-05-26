using InventorySystem.Interfaces;
using UI;
using UnityEngine;

namespace Crafting.Upgrade
{
    public class UpgradeTable : MonoBehaviour, IInteractable
    {
        public void Interact(IInteractor interactor)
        {
            UIManager.Instance.OpenUpgradeMenu();
            //CameraSwitcher.Instance.SwitchUpgradeCamera();
        }
    }
}
