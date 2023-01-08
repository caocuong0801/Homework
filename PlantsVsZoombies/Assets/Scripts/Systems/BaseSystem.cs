using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseSystem
{
    public bool IsInitialized { get; protected set; }

    public virtual void InitData() { }
    public virtual void Tick() { }
    public virtual void Clear() { }
}
