using System;
using System.Collections.Generic;
using UnityEngine;

namespace _RuinTheJam.Systems.InventorySystem
{
    [Serializable]
    public class Inventory
    {
        private readonly Dictionary<ItemTypes, int> _items = new();

        public Action<ItemTypes> OnItemAmountChanged;
        public Action<ItemTypes> OnItemAdded;
        public Action<ItemTypes> OnItemRemoved;

        public int Get(ItemTypes type)
        {
            _items.TryGetValue(type, out var value);
            return value;
        }

        public void Set(ItemTypes type, int newValue)
        {
            if (_items[type] == newValue) return;
            
            _items[type] = newValue;
            OnItemAmountChanged?.Invoke(type);
        }

        public void Add(ItemTypes type, int addedAmount)
        {
            if (_items.TryAdd(type, addedAmount))
            {
                OnItemAdded?.Invoke(type);
                return;
            }
            
            Set(type, Get(type) + addedAmount);
            OnItemAdded?.Invoke(type);
        }

        public void Remove(ItemTypes type, int amount)
        {
            if (Get(type) <= 0) return;

            if (Get(type) - amount <= 0)
            {
                Debug.LogWarning($"Not enough {type} in inventory");
                return;
            }
            
            Set(type, Get(type) - amount);
            OnItemRemoved?.Invoke(type);
        }
    }
    
    public enum ItemTypes
    {
        Scrap
    }
}