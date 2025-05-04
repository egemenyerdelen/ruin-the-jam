using Helpers;
using Player;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{


    [SerializeField] private GameObject drone;

    private PlayerDrone DR;
    [SerializeField] private GameObject player;

    private int scrap_count;

    public EntityDataHolder playerDataHolder;

    private void Start()
    {
        drone.GetComponent<PlayerDrone>();
    }

    public void BatteryUpgrade()
    {

        DR.batteryCap += 50;
    }

}
