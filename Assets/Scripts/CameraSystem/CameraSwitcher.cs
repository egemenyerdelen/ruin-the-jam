using Helpers;
using Unity.Cinemachine;

namespace CameraSystem
{
    public class CameraSwitcher : Singleton<CameraSwitcher>
    {
        public CinemachineCamera playerVCam;
        public CinemachineCamera droneVCam;

        private void Start()
        {
            // Set initial active cam
            ActivateDroneCam();
        }

        public void ActivatePlayerCam()
        {
            playerVCam.Priority = 10;
            droneVCam.Priority = 0;
        }

        public void ActivateDroneCam()
        {
            droneVCam.Priority = 10;
            playerVCam.Priority = 0;
        }
    }

}
