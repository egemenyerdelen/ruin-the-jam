using Helpers;
using Player;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{


    [SerializeField] private GameObject drone;

    private PlayerDrone DR;
    [SerializeField] private GameObject player;

    public EntityDataHolder dataHolder;

    private void Start()
    {
        drone.GetComponent<PlayerDrone>();
    }

    public void BatteryUpgrade()
    {

        DR.batteryCap += 50;
    }

    public void ScrapCapacity()
    {

        DR.scrapCapacity++;



    }




}
