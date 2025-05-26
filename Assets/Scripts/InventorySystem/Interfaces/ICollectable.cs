namespace InventorySystem.Interfaces
{
    public interface ICollectable
    {
        public void Collect(IInteractor interactor);
    }

    public class ItemTypeCountMatch
    {
        public ItemTypes ItemType;
        public int Count;
    }
}