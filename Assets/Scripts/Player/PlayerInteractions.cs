using System;
using CameraSystem;
using Input;
using InventorySystem;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        public bool canInteract = true;
        
        [SerializeField] private float maxDetectDistance = 5f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private Transform playerCameraTransform;

        private HighlightableItem _highlightableTarget;
        private IInteractable _interactableTarget;
        private IPickable _pickableTarget;

        private void Update()
        {
            if (!canInteract) {return;}
            
            DetectInteraction();

            if (InputManager.InputSystem.Player.Interact.IsPressed() || InputManager.InputSystem.Drone.Interact.IsPressed())
            {
                _interactableTarget?.Interact();
                _pickableTarget?.PickUp();
                
                _highlightableTarget?.DisableOutline();
                _highlightableTarget = null;
            }
            if (InputManager.InputSystem.Drone.Interact.IsPressed())
            {
                _interactableTarget?.Interact();
                _pickableTarget?.PickUp();
            }
        }
        
        private void DetectInteraction()
        {
            if (_highlightableTarget != null)
            {
                _highlightableTarget.DisableOutline();
                _highlightableTarget = null;
            }

            _interactableTarget = null;
            _pickableTarget = null;
            
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, 
                    out var hit, maxDetectDistance, interactableLayer))
            {
                if (hit.transform.CompareTag("Highlightable"))
                {
                    _highlightableTarget = hit.collider.GetComponent<HighlightableItem>();
                    
                    _highlightableTarget.EnableOutline();

                    if (_highlightableTarget is IInteractable interactable)
                    {
                        _interactableTarget = interactable;
                    }
                    
                    if (_highlightableTarget is IPickable pickableObject)
                    {
                        _pickableTarget = pickableObject;
                    }
                }
            }
        }
    }
}