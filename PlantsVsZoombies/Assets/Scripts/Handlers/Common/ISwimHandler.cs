using UnityEngine;

using System;

public interface ISwimHandler
{
    GameObject SwimObject { get; set; }
    float SwimSpeed { get; set; }
    bool IsSwimming { get; }

    event Action<Vector2> SwimmingDone; // callback with new position
    event Action<Vector2> SwimmingProgress; // callback with new position

    void StartSwimming();
}

