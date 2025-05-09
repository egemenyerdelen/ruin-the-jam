using CameraSystem;
using InventorySystem;
using UI;

namespace Upgrade
{
    public class UpgradeTable : HighlightableItem, IInteractable
    {
        public void Interact()
        {
            UIManager.Instance.OpenUpgradeMenu();
            //CameraSwitcher.Instance.SwitchUpgradeCamera();
        }
    }
}
