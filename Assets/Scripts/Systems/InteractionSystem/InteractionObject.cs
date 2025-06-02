using System.Collections.Generic;
using EntitySystem;
using InventorySystem;
using InventorySystem.Interfaces;
using Systems.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.InteractionSystem
{
    public class InteractionObject : MonoBehaviour, IInteractor
    {
        public Entity entity;
        public bool canInteract = true;

        public GameObject InteractorGameObject => gameObject;

        [SerializeField] private float maxDetectDistance = 5f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private Transform entityCameraTransform;
        
        private readonly Dictionary<Transform, CachedTargetData> _cache = new();
        private CachedTargetData _currentTarget;
        private Highlightable _highlightableTarget;
        private IInteractable _interactableTarget;

        private void Start()
        {
            InvokeRepeating(nameof(CleanupCache), 30, 15);
            
            InputManager.InputSystem.Player.Interact.performed += HandleInteraction;
            EntitySwitcher.OnEntitySwitched += SetInteractableOnEntitiesSwitched;
        }

        private void OnDisable()
        {
            InputManager.InputSystem.Player.Interact.performed -= HandleInteraction;
            EntitySwitcher.OnEntitySwitched -= SetInteractableOnEntitiesSwitched;
        }

        private void Update()
        {
            if (!canInteract) return;
            
            _currentTarget = DetectInteraction();
        }

        private void HandleInteraction(InputAction.CallbackContext context)
        {
            if (!canInteract || _currentTarget == null || _interactableTarget == null) return;
            
            _highlightableTarget.DisableHighlight();
            _interactableTarget.Interact(this);
            
            ResetTargets();
        }

        private void SetInteractableOnEntitiesSwitched(EntityType entityType)
        {
            canInteract = entityType == entity.entityType;
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
                
                if (cached.Highlightable != null && cached.Interactable != null)
                {
                    _cache[targetTransform] = cached;
                    _highlightableTarget = cached.Highlightable;
                    _interactableTarget = cached.Interactable;
                    _highlightableTarget?.EnableHighlight();
                }
            }

            return cached;
        }


        private void ResetTargets()
        {
            if (_highlightableTarget != null && _highlightableTarget.gameObject != null)
            {
                _highlightableTarget.DisableHighlight();
            }
            
            _interactableTarget = null;
            _highlightableTarget = null;
        }
        
        private void CleanupCache()
        {
            var keysToRemove = new List<Transform>();
            foreach (var kvp in _cache)
            {
                if (kvp.Key == null || kvp.Key.gameObject == null || kvp.Value.Highlightable == null 
                    || kvp.Value.Interactable == null)
                {
                    keysToRemove.Add(kvp.Key);
                }
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

        public CachedTargetData(Transform root)
        {
            Highlightable = root.GetComponent<Highlightable>();
            Interactable = root.GetComponent<IInteractable>();
        }
    }
}