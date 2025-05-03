using Helpers;

namespace CameraSystem
{
    public class CameraManager : Singleton<CameraManager>
    {
        public UnityEngine.Camera mainCamera;
        public UnityEngine.Camera playerCamera;
        public UnityEngine.Camera droneCamera;
        public UnityEngine.Camera upgradeTableCamera;

        public void EnableMainCamera()
        {
            mainCamera.enabled = true;
            playerCamera.enabled = false;
            droneCamera.enabled = false;
            upgradeTableCamera.enabled = false;
        }
        
        public void EnablePlayerCamera()
        {
            playerCamera.enabled = true;
            mainCamera.enabled = false;
            droneCamera.enabled = false;
            upgradeTableCamera.enabled = false;
        }
        
        public void EnableDroneCamera()
        {
            droneCamera.enabled = true;
            mainCamera.enabled = false;
            playerCamera.enabled = false;
            upgradeTableCamera.enabled = false;
        }
        
        public void EnableUpgradeCamera()
        {
            upgradeTableCamera.enabled = true;
            mainCamera.enabled = false;
            playerCamera.enabled = false;
            droneCamera.enabled = false;
        }
    }
}
