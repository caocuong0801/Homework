using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Load zoombie config of each stage
/// Handle count down to spaw zoombie waves
/// </summary>
public class ZoombieSystem : BaseSystem
{
    public Dictionary<Guid, IMapObjectModel> zoombiesOnMap = new Dictionary<Guid, IMapObjectModel>();

    public override void InitData()
    {
        base.InitData();
        IsInitialized = true;
    }


    public ZoombieModel AddZoobie(Vector2 position, int type, Guid id)
    {
        // Add zoombie in to system
        return null;
    }


    public void UpdateZoombiePosition(Guid id, Vector2 position)
    {
        if (!zoombiesOnMap.ContainsKey(id)) return;
        var zoombie = zoombiesOnMap[id];
        zoombie.Position = position;
    }
}
