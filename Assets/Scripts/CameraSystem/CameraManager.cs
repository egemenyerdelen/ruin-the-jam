using Helpers;

namespace CameraSystem
{
    public class CameraManager : Singleton<CameraManager>
    {
        public UnityEngine.Camera mainCamera;
        public UnityEngine.Camera playerCamera;
        public UnityEngine.Camera droneCamera;
        public UnityEngine.Camera upgradeTableCamera;

        public void SwitchMainCamera()
        {
            mainCamera.enabled = true;
            playerCamera.enabled = false;
            droneCamera.enabled = false;
            upgradeTableCamera.enabled = false;
        }
        
        public void SwitchPlayerCamera()
        {
            playerCamera.enabled = true;
            mainCamera.enabled = false;
            droneCamera.enabled = false;
            upgradeTableCamera.enabled = false;
        }
        
        public void SwitchDroneCamera()
        {
            droneCamera.enabled = true;
            mainCamera.enabled = false;
            playerCamera.enabled = false;
            upgradeTableCamera.enabled = false;
        }
        
        public void SwitchUpgradeCamera()
        {
            upgradeTableCamera.enabled = true;
            mainCamera.enabled = false;
            playerCamera.enabled = false;
            droneCamera.enabled = false;
        }
    }
}
