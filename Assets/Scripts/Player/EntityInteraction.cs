using Audio;
using Input;
using InventorySystem;
using UnityEngine;
using Upgrade;

namespace Player
{
    public class EntityInteraction : MonoBehaviour
    {
        public PlayerDrone droneScript;
        public bool canInteract = true;
        
        [SerializeField] private float maxDetectDistance = 5f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private Transform playerCameraTransform;

        private HighlightableItem _highlightableTarget;
        private IInteractable _interactableTarget;
        private HighlightableAndPickableItem _pickableTarget;
        private int _scrapHolding;

        private void Update()
        {
            if (!canInteract) {return;}
            
            DetectInteraction();

            if (InputManager.InputSystem.Player.Interact.IsPressed() && InputSwitcher.Instance.activeController == ControllerType.Player)
            {
                _interactableTarget?.Interact();
                // _pickableTarget?.PickUp();
            }
            if (InputManager.InputSystem.Drone.Interact.IsPressed() && InputSwitcher.Instance.activeController == ControllerType.Drone)
            {
                _interactableTarget?.Interact();

                if (_pickableTarget == null) return;
                
                // var typeCountMatch = _pickableTarget.GetItemDataMatch();
                if (_scrapHolding >= droneScript.scrapCapacity) return;

                AudioManager.Instance.PlaySoundFX("Scrap", 2);
                UpgradeManager.Instance.dataHolder.inventory.Add(ItemTypes.Scrap, 1);

                // _scrapHolding++;
                // Debug.Log(_scrapHolding);
                // Debug.Log(droneScript.scrapHolding);
                // droneScript.scrapHolding = _scrapHolding;
                
                _pickableTarget.PickUp();
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
                    
                    if (_highlightableTarget is HighlightableAndPickableItem pickableObject)
                    {
                        _pickableTarget = pickableObject;
                    }
                }
            }
        }
    }
}