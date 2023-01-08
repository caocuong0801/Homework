using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSystem : BaseSystem
{
    public override void InitData()
    {
        base.InitData();
        IsInitialized = true;
    }
}
