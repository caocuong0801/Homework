using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlantHandler : MonoBehaviour, IPlantHandler
{
    public IBaseAnimationHandler AnimationHandler { get; protected set; }

    public Guid Id { get; set; }
    public MapPlantModel Model { get;  private set; }

   
    private void FixedUpdate()
    {
        if (Model == null) return;
        transform.position = Model.Position;
    }


    public void SetModel(MapPlantModel model)
    {
        Model = model;
        AnimationHandler.SetModel(model);
    }
}
