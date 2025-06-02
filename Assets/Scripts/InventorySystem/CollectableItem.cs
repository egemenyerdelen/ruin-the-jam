using Core;
using InventorySystem.Interfaces;
using InventorySystem.Items;

namespace InventorySystem
{
    public class CollectableItem : Item, ICollectable, IInteractable
    {
        public void Interact(IInteractor interactor)
        {
            var inventory = interactor.InteractorGameObject.GetComponent<IInventoryHolder>().Inventory;
            
            Collect(inventory);
        }

        public void Collect(Inventory inventory)
        {
            inventory?.Add(itemType, itemCount);
            Destroy(gameObject);
        }
    }

}