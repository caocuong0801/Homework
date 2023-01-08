using System;
using System.Collections;
using UnityEngine;

public class JumpHandler : IJumpHandler
{
    public GameObject JumpObject { get; set; }
    public float JumpSpeed { get; set; }
    public float JumpHigh { get; set; }
    public bool IsJumping { get; protected set; }

    public event Action<Vector2> JumpDone; // callback with new position
    public event Action<Vector2> JumpingProgress; // callback with new position

    public void StartJumping()
    {
        IsJumping = true;
        if (JumpObject == null)
        {
            IsJumping = false;
            return;
        }

        /// maybe handleJump will take time
        handleJump();
        // jumping done
        JumpDone?.Invoke(JumpObject.transform.position);
        IsJumping = false;
    }


    private IEnumerable handleJump()
    {
        //.....
        var newPos = Vector2.zero;
        JumpingProgress?.Invoke(newPos);
        // while jumping is not done
        return null;
    }
}
