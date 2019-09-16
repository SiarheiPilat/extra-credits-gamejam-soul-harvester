using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUpgrader : Upgradeable
{
    public ClickDetector clickDetector;

    public override void IncreaseStrength(ulong u)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AutoHarvest", 0.0f, 1.0f);
    }

    void AutoHarvest()
    {
        clickDetector.HarvestSoul(0, new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-2.0f, 1.5f), -3.0f), (int)GetComponent<UpgradeButton>().Efficiency);
    }
}
