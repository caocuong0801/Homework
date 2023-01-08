using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Load sun config (max, starting count)
/// Handle sun countdown then send spawn sun event
/// Calculate sun count when collect/buy plants
/// </summary>
public class SunSystem : BaseSystem
{
    public override void InitData()
    {
        base.InitData();
        IsInitialized = true;
    }
}
