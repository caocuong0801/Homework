using System;
using UnityEngine;

public interface IBaseZoombieHandler
{
    IBaseAnimationHandler AnimationHandler { get; }

    Guid Id { get; set; }
    void SetModel(ZoombieModel model);
}