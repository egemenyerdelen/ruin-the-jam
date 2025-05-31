using System;
using EntitySystem.Drone;
using EntitySystem.Player;
using Helpers;
using UnityEngine;

namespace EntitySystem
{
    public class EntitySwitcher : Singleton<EntitySwitcher>
    {
        public EntityType activeEntity = EntityType.Player;
        
        [SerializeField] private PlayerInputProvider playerInputProvider;
        [SerializeField] private DroneInputProvider droneInputProvider;

        public static event Action<EntityType> OnEntitySwitched; 

        public void SwitchEntity(EntityType entityType)
        {
            if (activeEntity == entityType) return;
            
            switch (entityType)
            {
                case EntityType.Player:
                    
                    droneInputProvider.DisableInputs();
                    playerInputProvider.EnableInputs();

                    break;
                
                case EntityType.Drone:
                    
                    playerInputProvider.DisableInputs();
                    droneInputProvider.EnableInputs();
                    
                    break;
            }
            
            activeEntity = entityType;
            OnEntitySwitched?.Invoke(entityType);
        }
    }
}