using Helpers;
using Unity.Cinemachine;

namespace CameraSystem
{
    public class CameraSwitcher : Singleton<CameraSwitcher>
    {
        public CinemachineCamera playerVCam;
        public CinemachineCamera droneVCam;
        public CinemachineCamera droneThirdPerson;

        private void Start()
        {
            // Set initial active cam
            ActivateDroneThirdPerson();
        }

        public void ActivatePlayerCam()
        {
            playerVCam.Priority = 10;
            droneVCam.Priority = 0;
            droneThirdPerson.Priority = 0;
        }

        public void ActivateDroneCam()
        {
            droneVCam.Priority = 10;
            playerVCam.Priority = 0;
            droneThirdPerson.Priority = 0;
        }

        public void ActivateDroneThirdPerson()
        {
            droneThirdPerson.Priority = 10;
            droneVCam.Priority = 0;
            playerVCam.Priority = 0;
        }
    }

}
