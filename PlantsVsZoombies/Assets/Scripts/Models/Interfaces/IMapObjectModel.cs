using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapObjectModel : IBaseModel
{
    State State { get; set; }
    int HurtLevel { get; set; }
    Vector2 Position { get; set; }
    event Action<State> OnStateChanged;
}
