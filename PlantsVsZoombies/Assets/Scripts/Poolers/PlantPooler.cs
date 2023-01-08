using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hiraishin.Utilities;
using System;

public class PlantPooler : ObjectPooling
{
    private const string TAG_PREFIX = "Tree_";

    private string getTag (int type)
    {
        return TAG_PREFIX + type;
    }

    public GameObject GetObjectById(int type, Vector3 position, Quaternion quaternion)
    {
        var obj = SpawnObject(getTag(type), position, quaternion);
        return obj;
    }
}
