using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EatingTreeHandler : BasePlantHandler
{
    private const string ANIM_IDLE = "Idle";
    private const string ANIM_MOVE = "Move";
    private const string ANIM_ATTACK = "Attack";
    private const string ANIM_DEATH = "Death";

    public EatingTreeHandler([Inject(Id = "Sunflower")] IBaseAnimationHandler animationHandler)
    {
        if (animationHandler == null) return;

        this.AnimationHandler = animationHandler;

        var animDics = new Dictionary<State, string>();
        animDics[State.Idling] = ANIM_IDLE;
        animDics[State.Moving] = ANIM_MOVE;
        animDics[State.Attacking] = ANIM_ATTACK;
        animDics[State.Death] = ANIM_DEATH;

        AnimationHandler.SetAnimationNames(animDics);
        AnimationHandler.SetIdleAnim(ANIM_IDLE);
    }
}
