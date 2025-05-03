namespace InventorySystem
{
    public abstract class HighlightableAndPickableItem : Item, IPickable
    {
        public Outline outline;
        public ItemTypes itemType;
        
        public ItemTypeCountMatch GetItemDataMatch()
        {
            var match = new ItemTypeCountMatch
            {
                ItemType = itemType,
                Count = 1
            };
            return match;
        }
        
        public void EnableOutline()
        {
            outline.enabled = true;
        }

        public void DisableOutline()
        {
            outline.enabled = false;
        }

        public abstract void PickUp();
    }
}