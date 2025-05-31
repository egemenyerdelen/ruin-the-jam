using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        private readonly Dictionary<ItemTypes, int> _items = new();

        public Action<ItemTypes, int> OnItemAdded;

        public int Get(ItemTypes type)
        {
            _items.TryGetValue(type, out var value);
            return value;
        }

        public void Set(ItemTypes type, int newValue)
        {
            _items[type] = newValue;
        }

        public void Add(ItemTypes type, int addedAmount)
        {
            if (_items.TryAdd(type, addedAmount))
            {
                OnItemAdded?.Invoke(type, addedAmount);
                return;
            }
            
            Set(type, Get(type) + addedAmount);
            OnItemAdded?.Invoke(type, addedAmount);
        }
    }
    
    public enum ItemTypes
    {
        Scrap
    }
}