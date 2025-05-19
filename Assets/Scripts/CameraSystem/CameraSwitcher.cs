using System;
using Helpers;
using Systems.Input;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem
{
    public class CameraSwitcher : Singleton<CameraSwitcher>
    {
        public CinemachineCamera playerVCam;
        public CinemachineCamera droneVCam;
        public CinemachineCamera droneThirdPerson;

        [SerializeField] private CustomCameraTypes activeCameraType = CustomCameraTypes.DroneFirstPerson;
        
        private void Start()
        {
            InputManager.InputSystem.Drone.ChangeCamera.performed += ChangeDroneCameraByInput;
            
            ActivateTargetCamera(activeCameraType);
        }

        private void OnDisable()
        {
            InputManager.InputSystem.Drone.ChangeCamera.performed -= ChangeDroneCameraByInput;
        }

        private void ChangeDroneCameraByInput(InputAction.CallbackContext context)
        {
            switch (activeCameraType)
            {
                case CustomCameraTypes.DroneFirstPerson:
                    ActivateDroneThirdPerson();
                    break;
                case CustomCameraTypes.DroneThirdPerson:
                    ActivateDroneFirstPersonCam();
                    break;
            }
        }
        
        public void ActivateTargetCamera(CustomCameraTypes cameraTypesEnum)
        {
            switch (cameraTypesEnum)
            {
                case CustomCameraTypes.PlayerFirstPerson:
                    ActivatePlayerCam();
                    break;
                case CustomCameraTypes.DroneFirstPerson:
                    ActivateDroneFirstPersonCam();
                    break;
                case CustomCameraTypes.DroneThirdPerson:
                    ActivateDroneThirdPerson();
                    break;
                case CustomCameraTypes.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ActivatePlayerCam()
        {
            playerVCam.Priority = 10;
            droneVCam.Priority = 0;
            droneThirdPerson.Priority = 0;

            activeCameraType = CustomCameraTypes.PlayerFirstPerson;
        }

        private void ActivateDroneFirstPersonCam()
        {
            droneVCam.Priority = 10;
            playerVCam.Priority = 0;
            droneThirdPerson.Priority = 0;

            activeCameraType = CustomCameraTypes.DroneFirstPerson;
        }

        private void ActivateDroneThirdPerson()
        {
            droneThirdPerson.Priority = 10;
            droneVCam.Priority = 0;
            playerVCam.Priority = 0;
            
            activeCameraType = CustomCameraTypes.DroneThirdPerson;
        }
    }

    public enum CustomCameraTypes
    {
        PlayerFirstPerson,
        DroneFirstPerson,
        DroneThirdPerson,
        Null
    }
}