using System;
using UnityEngine;

public interface IJumpHandler
{
    GameObject JumpObject { get; set; }
    float JumpSpeed { get; set; }
    float JumpHigh { get; set; }
    bool IsJumping { get; }

    event Action<Vector2> JumpDone; // callback with new position
    event Action<Vector2> JumpingProgress; // callback with new position

    void StartJumping();
}
