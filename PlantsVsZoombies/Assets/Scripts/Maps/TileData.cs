using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    None,
    Ground,
    Grass,
    Water,
    Stair
}

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileType Type;
    public TileBase[] Tiles;

    public bool CanPutPlant()
    {
        return Type == TileType.Grass;
    }
}