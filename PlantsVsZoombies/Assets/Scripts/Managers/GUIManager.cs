using System;
using UnityEngine;
using NBCore;

/// <summary>
/// Handle the UI viewing & interaction
/// </summary>
public class GUIManager : SingletonMono<GUIManager>
{
    private IStoreObjectModel DraggingData;

    /// <summary>
    /// This event will be invoke when user drag & drop a plant on the map
    /// </summary>
    public event Action<IStoreObjectModel, Vector2> OnAddedPlant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void handleTouchStart()
    {
        // Check if user touch on a plant in the store
        // Set DraggingData is the data of plant in the store

        // -- DraggingData = touchedObject data --
    }
    private void handleTouchMode() { } // Move dragging object
    private void handleTouchEnd(Vector3 position)
    {
        OnAddedPlant(DraggingData, position);
    }
}
