using InventorySystem.Interfaces;
using UnityEngine;

namespace InventorySystem.Items
{
    public class Item : MonoBehaviour
    {
        public ItemTypes itemType;
        public int itemCount = 1;
        
        public ItemTypeCountMatch GetItemDataMatch()
        {
            var match = new ItemTypeCountMatch
            {
                ItemType = itemType,
                Count = itemCount
            };
            return match;
        }
    }
}