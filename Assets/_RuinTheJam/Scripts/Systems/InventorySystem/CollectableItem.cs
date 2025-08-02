using _RuinTheJam.Core;
using _RuinTheJam.Systems.InteractionSystem.Interfaces;
using _RuinTheJam.Systems.InventorySystem.Interfaces;
using _RuinTheJam.Systems.InventorySystem.Items;

namespace _RuinTheJam.Systems.InventorySystem
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