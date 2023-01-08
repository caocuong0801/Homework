using System;
using UnityEngine;
using System.Collections.Generic;
using Hiraishin.Utilities;

public class ItemInPooler : MonoBehaviour
{
    // Must set PoolerTag before cache
    // This PoolerTag will be used for building cache key.
    public string PoolerTag;
    public string CachedKey;
}

public class Pooler<T> : ObjectPooling where T : ItemInPooler
{
    private int currentIndex = 1;
    private Dictionary<string, T> spawnedItems = new Dictionary<string, T>();

    public string BuildKey(string id)
    {
        var result = $"{id}_{currentIndex}";
        currentIndex++;
        return result;
    }

    public void CacheSpawnedItem(T item)
    {
        item.CachedKey = BuildKey(item.PoolerTag);
        spawnedItems[item.CachedKey] = item;
    }

    public void Clear()
    {
        foreach (var item in spawnedItems)
        {
            // Debug.Log($"Clear id ======== {item.Key}");
            var obj = item.Value.gameObject;
            if (obj != null)
            {
                var id = obj.GetComponent<T>()?.PoolerTag;
                // Debug.Log($"Clear id ======== {id}");
                ReturnToPool(id, obj, true);
            }
        }
        spawnedItems.Clear();
    }

    public void ReturnToPoolAndRemoveFromCache(T item, bool hideNow = false)
    {
        ReturnToPool(item.PoolerTag, item.gameObject, hideNow);
        if (spawnedItems.ContainsKey(item.CachedKey))
        {
            spawnedItems.Remove(item.CachedKey);
        }
    }

    public GameObject SpawnAndAddIntoCache(string tagInPool, Vector3 position)
    {
        GameObject obj = SpawnObject(tagInPool, position, Quaternion.identity);
        T itemCtrl = obj.GetComponent<T>();
        if (itemCtrl != null)
        {
            itemCtrl.PoolerTag = tagInPool;
            CacheSpawnedItem(itemCtrl);
        }
        return obj;
    }

}
