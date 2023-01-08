using System;
using UnityEngine;
using NBCore;
using Zenject;

public class PlantManager : SingletonMono<PlantManager>
{
    [Inject] private PlantPooler plantPooler;
    [Inject] private PlantSystem plantSystem;


    private bool checkAvaiablePosition(Vector2 position, int type)
    {
        // Call map system to check if this position has enought empty space => return true
        // If PlantSystem has plant at this tile, check if the plant can be upgraded => return true
        // Return false
        return false;
    }
    

    public GameObject AddPlant(Vector2 position, int type)
    {
       if (!checkAvaiablePosition(position, type))
        {
            return null;
        }

        return plantPooler.GetObjectById(type, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

}
