using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Interfaces;
using Systems.Input;
using UnityEngine;

namespace EntitySystem
{
    public class EntityInteraction : MonoBehaviour
    {
        public Entity entity;
        public bool canInteract = true;
        
        [SerializeField] protected float maxDetectDistance = 5f;
        [SerializeField] protected LayerMask interactableLayer;
        [SerializeField] protected Transform entityCameraTransform;

        private readonly Dictionary<Transform, CachedTargetData> _cache = new();
        private CachedTargetData _currentTarget;
        private Transform _previousTargetTransform;
        private Highlightable _highlightableTarget;
        private IInteractable _interactableTarget;
        
        private void Start()
        {
            InvokeRepeating(nameof(CleanupCache), 30, 15);
        }

        private void Update()
        {
            if (!canInteract) return;
            
            _currentTarget = DetectInteraction();
            
            HandleInteraction();
        }
        
        private void HandleInteraction()
        {
            if (_currentTarget == null || !InputManager.InputSystem.Player.Interact.IsPressed()) return;
            
            _currentTarget.Interactable?.Interact(entity);
                
            ResetTargets();
        }

        private CachedTargetData DetectInteraction()
        {
            ResetTargets();
            
            if (!Physics.Raycast(entityCameraTransform.position, entityCameraTransform.forward, out var hit,
                    maxDetectDistance, interactableLayer)) return null;

            var targetTransform = hit.collider.transform;

            if (_cache.TryGetValue(targetTransform, out var cached))
            {
                _highlightableTarget = cached.Highlightable;
                _interactableTarget = cached.Interactable;

                _highlightableTarget?.EnableHighlight();
            }
            else
            {
                cached = new CachedTargetData(targetTransform);
                _cache[targetTransform] = cached;
            }

            return cached;
        }


        private void ResetTargets()
        {
            _highlightableTarget?.DisableHighlight();
            _highlightableTarget = null;
            _interactableTarget = null;
        }
        
        private void CleanupCache()
        {
            var keysToRemove = new List<Transform>();
            foreach (var kvp in _cache)
            {
                if (kvp.Key == null)
                    keysToRemove.Add(kvp.Key);
            }

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }
    }
    
    public class CachedTargetData
    {
        public readonly Highlightable Highlightable;
        public readonly IInteractable Interactable;
        public readonly CollectableItem CollectableItem;

        public CachedTargetData(Transform root)
        {
            Highlightable = root.GetComponent<Highlightable>();
            Interactable = root.GetComponent<IInteractable>();
            CollectableItem = root.GetComponent<CollectableItem>();
        }
    }
}