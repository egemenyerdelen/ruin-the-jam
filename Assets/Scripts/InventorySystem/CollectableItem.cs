using InventorySystem.Interfaces;
using InventorySystem.Items;

namespace InventorySystem
{
    public class CollectableItem : Item, ICollectable, IInteractable
    {
        public void Interact(IInteractor interactor)
        {
            Collect(interactor);
        }

        public void Collect(IInteractor interactor)
        {
            interactor.EntityDataHolder.inventory.Add(itemType, itemCount);
            Destroy(gameObject);
        }
    }

}