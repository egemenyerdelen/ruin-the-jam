using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        private readonly Dictionary<ItemTypes, int> _items = new();

        public static Action<ItemTypes, int> OnItemAdded;

        public int Get(ItemTypes type)
        {
            _items.TryGetValue(type, out var value);
            return value;
        }

        public void Set(ItemTypes type, int newValue)
        {
            var currentValue = Get(type);

            if (newValue > currentValue)
            {
                var addedAmount = newValue - currentValue;
                _items[type] = newValue;
                OnItemAdded?.Invoke(type, addedAmount);
            }
            else
            {
                _items[type] = newValue;
            }
        }

        public void Add(ItemTypes type, int amount)
        {
            if (_items.TryAdd(type, amount))
            {
                Debug.Log($"{type} = {_items[type]}");
                return;
            }
            
            Set(type, Get(type) + amount);
        }
    }
    
    public enum ItemTypes
    {
        Scrap
    }
}