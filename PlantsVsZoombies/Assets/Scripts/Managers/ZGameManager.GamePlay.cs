using System;
using UnityEngine;
using NBCore;

public partial class ZGameManager : SingletonMono<ZGameManager>
{
    private void handleAddPlant(IStoreObjectModel data, Vector2 position)
    {
        var obj = PlantManager.Instance.AddPlant(position, data.Type);
        if (obj != null)
        {
            var id = new Guid();
            var model = plantSystem.AddPlant(position, data.Type, id);
            var handler = obj.GetComponent<IPlantHandler>();

            if (handler != null)
            {
                handler.Id = id;
                handler.SetModel(model);
            }
        }
    }
}
