using Input;
using InventorySystem;
using UnityEngine;

namespace Player
{
    public class PlayerPickup : MonoBehaviour
    {
        [SerializeField] private float maxDetectDistance = 5f;
        [SerializeField] private LayerMask pickableLayer;
        [SerializeField] private Transform cameraTransform;

        private IPickable _currentTarget;

        private void Update()
        {
            DetectPickable();

            if (_currentTarget != null && InputManager.InputSystem.Player.Interact.IsPressed())
            {
                _currentTarget.PickUp();
                // _currentTarget.ShowOutline(false);
                _currentTarget = null;
            }
        }

        private void DetectPickable()
        {
            // if (_currentTarget != null)
            //     _currentTarget.ShowOutline(false); // Remove outline from previous

            // _currentTarget = null;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, maxDetectDistance, pickableLayer))
            {
                
                if (hit.transform.CompareTag("Pickable"))
                {
                    var pickable = hit.collider.GetComponent<IPickable>();
                    
                    _currentTarget = pickable;
                    // _currentTarget.ShowOutline(true);
                    Debug.Log("Target object is pickable!");
                }
            }
        }
    }
}