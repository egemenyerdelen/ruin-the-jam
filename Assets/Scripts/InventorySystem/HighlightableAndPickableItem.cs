namespace InventorySystem
{
    public abstract class HighlightableAndPickableItem : HighlightableItem, IPickable
    {
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

        public abstract void PickUp();
    }
}