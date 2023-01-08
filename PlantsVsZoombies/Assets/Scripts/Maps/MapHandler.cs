using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;

public class MapHandler : MonoBehaviour
{
    [SerializeField] private List<TileData> tileDatas;

    [SerializeField] public Tilemap tileMap;


    private Dictionary<TileBase, TileData> dataFromTiles = new Dictionary<TileBase, TileData>();

    private void cacheTileDatas()
    {
        if (tileDatas != null)
        {
            foreach (var tileData in tileDatas)
            {
                foreach (var tile in tileData.Tiles)
                {
                    dataFromTiles[tile] = tileData;
                }
            }
        }
    }


    public bool CanPutPlant(Vector2 position)
    {
        if (tileMap == null) return false;

        var tilePos = tileMap.WorldToCell(position);
        var tileData = GetTileData(tilePos);
        return tileData.CanPutPlant();
    }


    public Vector2 GetCenterTile(Vector2 position)
    {
        if (tileMap == null) return Vector2.zero;

        var tilePos = tileMap.WorldToCell(position);
        return tileMap.GetCellCenterWorld(tilePos);
    }


    public TileData GetTileData(Vector3Int gridPosition)
    {
        if (tileMap == null) return null;

        TileBase tile = tileMap.GetTile(gridPosition);
        //Debug.Log("get tile : " + gridPosition + "===" + tile?.name);

        if (tile != null && dataFromTiles.ContainsKey(tile))
        {
            return dataFromTiles[tile];
        }
        return null;
    }


    public string ExportToJson()
    {
#if UNITY_EDITOR
        cacheTileDatas();
        Dictionary<string, int> objs = new Dictionary<string, int>();
        
        // dynamic size in pve
        Debug.LogWarning("export : " + tileMap.cellBounds);
        var maxX = tileMap.cellBounds.size.x;
        var maxY = tileMap.cellBounds.size.y;


        int[,] temp = new int[maxY, maxX];

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                TileData tileData = GetTileData(new Vector3Int(x, y, 0));
                Debug.Log($"Tile data at {x}, {y} = {tileData}");
                if (tileData != null)
                {
                    temp[y, x] = (int)tileData.Type;
                }
                else
                {
                    temp[y, x] = (int)TileType.None;
                }
            }
        }
        return JsonConvert.SerializeObject(temp);
        // Debug.Log($"Temp = {test}");
#endif
        return string.Empty;
    }
}
