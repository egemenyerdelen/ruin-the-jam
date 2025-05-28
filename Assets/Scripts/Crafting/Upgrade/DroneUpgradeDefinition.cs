using UnityEngine;

namespace Crafting.Upgrade
{
    [CreateAssetMenu(menuName = "Upgrades/DroneUpgradeDefinition")]
    public class DroneUpgradeDefinition : ScriptableObject
    {
        public DroneUpgradeType type;
        public int amount;
        public int cost;
        // public string title;
        public string description;
        public Sprite icon;
    }
    
    public enum DroneUpgradeType
    {
        Battery,
        Range,
        ThrustPower,
        Agility,
        ScrapCapacity
    }
}