namespace InventorySystem.Items
{
    public class Scrap : HighlightableAndPickableItem
    {
        public override void PickUp()
        {
            Destroy(gameObject);
        }
    }
}