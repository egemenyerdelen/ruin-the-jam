namespace InventorySystem
{
    public interface IPickable
    {
        public void PickUp();
    }

    public class ItemTypeCountMatch
    {
        public ItemTypes ItemType;
        public int Count;
    }
}