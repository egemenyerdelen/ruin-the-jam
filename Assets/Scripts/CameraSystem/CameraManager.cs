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
            mainCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            droneCamera.gameObject.SetActive(false);
            upgradeTableCamera.gameObject.SetActive(false);
        }
        
        public void EnablePlayerCamera()
        {
            mainCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            droneCamera.gameObject.SetActive(false);
            upgradeTableCamera.gameObject.SetActive(false);
        }
        
        public void EnableDroneCamera()
        {
            mainCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(false);
            droneCamera.gameObject.SetActive(true);
            upgradeTableCamera.gameObject.SetActive(false);
        }
        
        public void EnableUpgradeCamera()
        {
            mainCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(false);
            droneCamera.gameObject.SetActive(false);
            upgradeTableCamera.gameObject.SetActive(true);
        }
    }
}
