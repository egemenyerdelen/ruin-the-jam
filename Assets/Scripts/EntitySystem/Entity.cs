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
        
        public string ID { get; private set; }

        private void Awake()
        {
            ID = System.Guid.NewGuid().ToString();
            EntityManager.AddEntityToCatalog(this);
        }

        private void OnDestroy()
        {
            EntityManager.RemoveEntityFromCatalog(this);
        }
    }

}