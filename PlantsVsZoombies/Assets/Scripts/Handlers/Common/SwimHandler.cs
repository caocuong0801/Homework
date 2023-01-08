using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimHandler : MonoBehaviour
{
    public GameObject SwimObject { get; set; }
    public float SwimSpeed { get; set; }
    public bool IsSwimming { get; protected set; }

    public event Action<Vector2> SwimmingDone; // callback with new position
    public event Action<Vector2> SwimmingProgress; // callback with new position

    public void StartSwimming()
    {

    }

    public void StartSwiming()
    {
        IsSwimming = true;
        if (SwimObject == null)
        {
            IsSwimming = false;
            return;
        }

        /// maybe handleSwim will take time
        handleSwim();
        // Swiming done
        SwimmingDone?.Invoke(SwimObject.transform.position);
        IsSwimming = false;
    }


    private IEnumerable handleSwim()
    {
        //.....
        var newPos = Vector2.zero;
        SwimmingProgress?.Invoke(newPos);
        // while Swiming is not done
        return null;
    }
}
