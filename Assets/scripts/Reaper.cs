using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    Animator an;
    public float RepeatRate;
    public Transform soulStealPlace;
    public ClickDetector clickDetector;
    public int ReaperStrength;

    void Start()
    {
        an = GetComponent<Animator>();
        InvokeRepeating("Attack", 0.0f, RepeatRate);
    }

    void Attack()
    {
        an.Play("reaper-attack");
    }

    void ReaperHarvest()
    {
        clickDetector.HarvestSoul(0, soulStealPlace.position, ReaperStrength);
    }
}
