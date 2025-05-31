using InventorySystem.Interfaces;
using UnityEngine;

namespace EntitySystem
{
    public abstract class Entity : MonoBehaviour, IInteractor
    {
        public EntityType entityType;
        public EntityDataHolder EntityDataHolder => entityDataHolder;
        
        [SerializeField] 
        private EntityDataHolder entityDataHolder;

        private void Awake()
        {
            EntityManager.AddEntityToCatalog(this);
        }

        private void OnDestroy()
        {
            EntityManager.RemoveEntityFromCatalog(this);
        }
    }

}