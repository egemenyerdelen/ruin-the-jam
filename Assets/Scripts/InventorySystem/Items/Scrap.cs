using UnityEngine;

namespace InventorySystem.Items
{
    public class Scrap : MonoBehaviour, IPickable
    {
        public ItemTypes itemType = ItemTypes.Scrap;
        
        public void PickUp()
        {
            Destroy(gameObject);
        }
    }
}