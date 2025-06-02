using Core;
using InventorySystem;
using UnityEngine;

namespace EntitySystem
{
    public abstract class Entity : MonoBehaviour, IInventoryHolder
    {
        public EntityType entityType;
        public Inventory Inventory { get; } = new ();
        
        private void Awake()
        {
            EntityManager.AddEntityToCatalog(this);
        }

        private void OnDestroy()
        {
            EntityManager.RemoveEntityFromCatalog(this);
        }
    }
    
    public enum EntityType
    {
        Player,
        Drone
    }

}