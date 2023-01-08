using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Handle load data from config for plants (avaiable plants, 
/// Check if plant is avaiable (unlocked) to plant on map
/// </summary>
public class PlantSystem : BaseSystem
{
    [Inject] MapSystem mapSystem;

    public Dictionary<int, IStoreObjectModel> plantsInStore = new Dictionary<int, IStoreObjectModel>();
    public Dictionary<Guid, IMapObjectModel> plantsOnMap = new Dictionary<Guid, IMapObjectModel>();

    public event Action<IMapObjectModel> OnPlantAdded;
    public event Action<IMapObjectModel> OnPlantRevmoed;

    public override void InitData()
    {
        base.InitData();
        IsInitialized = true;

        if (ConfigManager.Instance.stageData != null)
        {
            // create plant in store data
            var plantDatas = ConfigManager.Instance.stageData.GetPlants();
            foreach (var data in plantDatas)
            {
                var plant = new StorePlantModel();
                plant.Parse(data);
                plantsInStore[plant.Type] = plant;
            }
        }
    }


    public MapPlantModel AddPlant(Vector2 position, int type, Guid id)
    {
        if (!plantsInStore.ContainsKey(type)) return null;

        var plantInStore = plantsInStore[type];
        var newPlant = new MapPlantModel();
        newPlant.ID = id;
        newPlant.Parse(plantInStore);
        // Put plant at center of tile
        newPlant.Position = mapSystem.GetCenterTile(position);
        plantsOnMap[id] = newPlant;
        return newPlant;
    }


    public override void Clear()
    {
        base.Clear();
        plantsInStore.Clear();
        plantsOnMap.Clear();

    }
}
