using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Load map config (id, type, size)
/// Handle load, draw, clear map
/// </summary>
public class MapSystem : BaseSystem
{
    private Tilemap tileMap;
    private MapHandler mapHandler;

    public override void InitData()
    {
        base.InitData();
        IsInitialized = true;
    }


    public bool CanPlantObject(Vector2 position)
    {
        return mapHandler.CanPutPlant(position);
    }


    public Vector2 GetCenterTile(Vector2 position)
    {
        return mapHandler.GetCenterTile(position);
    }


    public void SetTilemap(Tilemap map)
    {
        tileMap = map;
        if (tileMap != null)
        {
            mapHandler = tileMap.GetComponent<MapHandler>();
        }
    }
}
