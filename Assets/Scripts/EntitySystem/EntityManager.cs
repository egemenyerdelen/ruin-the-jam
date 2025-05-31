using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace EntitySystem
{
    public class EntityManager : Singleton<EntityManager>
    {
        private static readonly List<Entity> EntityCatalog = new();

        public static void AddEntityToCatalog(Entity entity)
        {
            EntityCatalog.Add(entity);
        }

        public static void RemoveEntityFromCatalog(Entity entity)
        {
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

        public static int GetTotalEntityCount()
        {
            return EntityCatalog.Count;
        }
    }

}