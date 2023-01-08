using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZoombieCanJumpAndSwimHandler : BaseZoombieHandler
{
    [Inject] IJumpHandler jumpHandler;
    [Inject] ISwimHandler swimHandler;
    [Inject] ZoombieSystem zoombieSystem;

    private void OnDestroy()
    {
        jumpHandler.JumpingProgress -= handleJumpingProgress;
        swimHandler.SwimmingProgress -= handleJumpingProgress;
    }


    protected override void handleAwake()
    {
        base.handleAwake();
        //jumpHandler.JumpSpeed = Model.JumpSpeed;
        //jumpHandler.JumpHigh = Model.JumpHigh;
        jumpHandler.JumpingProgress += handleJumpingProgress;
        swimHandler.SwimmingProgress += handleJumpingProgress;
    }


    protected override void handleFixedUpdate()
    {
        if (!Model.Jumpable) return;

        if (!jumpHandler.IsJumping)
        {
            base.handleFixedUpdate();
            if (Model.State == State.Jumping)
            {
                jumpHandler.StartJumping();
            }
        }
    }


    private void handleJumpingProgress(Vector2 position)
    {
        zoombieSystem.UpdateZoombiePosition(Model.ID, position);
    }
}
