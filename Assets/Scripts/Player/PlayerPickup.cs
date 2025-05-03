using Input;
using InventorySystem;
using UnityEngine;

namespace Player
{
    public class PlayerPickup : MonoBehaviour
    {
        [SerializeField] private EntityDataHolder playerDataHolder;
        [SerializeField] private float maxDetectDistance = 5f;
        [SerializeField] private LayerMask pickableLayer;
        [SerializeField] private Transform cameraTransform;
        
        private HighlightableAndPickableItem _currentTarget;

        private void Update()
        {
            DetectPickable();

            if (_currentTarget != null && InputManager.InputSystem.Player.Interact.IsPressed())
            {
                var typeCountMatch = _currentTarget.GetItemDataMatch();
                playerDataHolder.inventory.Add(typeCountMatch.ItemType, typeCountMatch.Count);

                _currentTarget.DisableOutline();
                _currentTarget.PickUp();
                _currentTarget = null;
            }
        }

        private void DetectPickable()
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, maxDetectDistance, pickableLayer))
            {
                if (hit.transform.CompareTag("Pickable"))
                {
                    var pickable = hit.collider.GetComponent<HighlightableAndPickableItem>();
                    
                    _currentTarget = pickable;
                    _currentTarget.EnableOutline();
                }
                else
                {
                    if (_currentTarget == null) return;
                    
                    _currentTarget.DisableOutline();
                    _currentTarget = null;
                }
            }
        }
    }
}