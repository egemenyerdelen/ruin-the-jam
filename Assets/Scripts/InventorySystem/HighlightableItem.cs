namespace InventorySystem
{
    public class HighlightableItem : Item
    {
        public Outline outline;
        
        public void EnableOutline()
        {
            outline.enabled = true;
        }

        public void DisableOutline()
        {
            outline.enabled = false;
        }
    }
}