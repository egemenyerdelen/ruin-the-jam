using InventorySystem;
using UnityEngine;

namespace EntitySystem
{
    public class EntityDataHolder : MonoBehaviour
    {
        public Inventory inventory = new();
    }

    public enum EntityType
    {
        Player,
        Drone
    }
}