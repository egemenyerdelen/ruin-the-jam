using System;
using EntitySystem;
using UnityEngine;

namespace UI
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject playerUI;
        [SerializeField] private GameObject droneHUD;

        private void Start()
        {
            EntitySwitcher.OnEntitySwitched += ChangeUIOnControllerSwitched;
        }

        private void OnDisable()
        {
            EntitySwitcher.OnEntitySwitched -= ChangeUIOnControllerSwitched;
        }

        private void ChangeUIOnControllerSwitched(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.Player:
                    ActivatePlayerUI();
                    break;
                case EntityType.Drone:
                    ActivateDroneUI();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }

        private void ActivatePlayerUI()
        {
            droneHUD.SetActive(false);
            playerUI.SetActive(true);
        }

        private void ActivateDroneUI()
        {
            playerUI.SetActive(false);
            droneHUD.SetActive(true);
        }
    }
}