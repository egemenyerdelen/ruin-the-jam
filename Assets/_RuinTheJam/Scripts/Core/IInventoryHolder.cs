using _RuinTheJam.Systems.InventorySystem;

namespace _RuinTheJam.Core
{
    public interface IInventoryHolder
    {
        public Inventory Inventory { get; }
    }
}