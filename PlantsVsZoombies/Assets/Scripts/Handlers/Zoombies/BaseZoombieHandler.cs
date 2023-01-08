using System;
using UnityEngine;

public class BaseZoombieHandler : MonoBehaviour, IBaseZoombieHandler
{
    public IBaseAnimationHandler AnimationHandler { get; protected set; }

    public Guid Id { get; set; }
    public ZoombieModel Model { get; private set; }


    private void Awake()
    {
        handleAwake();
    }


    protected virtual void handleAwake() {}


    private void FixedUpdate()
    {
        if (Model == null) return;
    }


    protected virtual void handleFixedUpdate()
    {
        transform.position = Model.Position;
    }


    public void SetModel(ZoombieModel model)
    {
        Model = model;
        AnimationHandler.SetModel(model);
    }
}
