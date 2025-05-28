using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace EntitySystem
{
    public class EntityManager : Singleton<EntityManager>
    {
        private static readonly Dictionary<string, Entity> EntityByIDDictionary = new();
        private static readonly List<Entity> EntityCatalog = new();

        public static void AddEntityToCatalog(Entity entity)
        {
            if (EntityByIDDictionary.TryAdd(entity.ID, entity))
            {
                EntityCatalog.Add(entity);
            }
        }

        public static void RemoveEntityFromCatalog(Entity entity)
        {
            EntityByIDDictionary.Remove(entity.ID);
            EntityCatalog.Remove(entity);
        }

        public static Entity GetFirstEntityOfType(EntityType type)
        {
            return EntityCatalog.FirstOrDefault(e => e.entityType == type);
        }

        public static List<Entity> GetAllEntitiesOfType(EntityType type)
        {
            return EntityCatalog.Where(e => e.entityType == type).ToList();
        }

        public static Entity GetEntityByID(string id)
        {
            return EntityCatalog.FirstOrDefault(e => e.ID == id);
        }

        public static int GetTotalEntityCount()
        {
            return EntityCatalog.Count;
        }
    }

}