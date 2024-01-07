using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PLAYER_ITEMS : MonoBehaviour
{
    //POSSE
    [SerializeField] private int currentWood;
    [SerializeField] private int currentCrop;
    [SerializeField] private float currentWater;
    //LIMITES 
    [SerializeField] private float waterLimit = 50f;
    [SerializeField] private float woodLimit = 50f;
    [SerializeField] private float cropLimit = 50f;

    public int propCurrentWood { get => currentWood; set => currentWood = value; }
    public int propCurrentCrop { get => currentCrop; set => currentCrop = value; }
    public float propCurrentWater { get => currentWater; set => currentWater = value; }
    public float propWaterLimit { get => waterLimit; set => waterLimit = value; }
    public float propWoodLimit { get => woodLimit; set => woodLimit = value; }
    public float propCropLimit { get => cropLimit; set => cropLimit = value; }

    public void WaterLimit(int water)
    {
        if (currentWater < waterLimit)
        {
            currentWater += water;
        }
    }
}
