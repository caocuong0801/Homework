using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer
{
    public void Play(string name, bool loop)
    {
        Debug.Log("Playing animation " + name);
    }
}

public class BaseAnimationHandler : IBaseAnimationHandler
{
    /// <summary>
    /// AnimationPlayer is a testing class.
    /// If we use SpriteSheet we need to define a class to play SpriteSheet.
    /// Or if we use Spine we need to use SkeletonAnimation.
    /// </summary>
    private AnimationPlayer animPlayer;
    private string idleAnim = string.Empty;
    private string currentAnim = string.Empty;

    private Dictionary<State, string> animDicts = new Dictionary<State, string>();
    private IMapObjectModel model;


    public void SetAnimationNames(Dictionary<State, string> anims)
    {
        animDicts = anims;
    }


    public void SetIdleAnim(string idleAnim)
    {
        this.idleAnim = idleAnim;
    }


    public void SetModel(IMapObjectModel model)
    {
        this.model = model;
    }


    // Update is called once per frame
    void Update()
    {
        if (model == null) return;
        var anim = animDicts.ContainsKey(model.State) ? animDicts[model.State] : idleAnim;
        if (!anim.Equals(currentAnim))
        {
            animPlayer.Play(anim, anim.Equals(idleAnim));
            currentAnim = anim;
        }
    }
}