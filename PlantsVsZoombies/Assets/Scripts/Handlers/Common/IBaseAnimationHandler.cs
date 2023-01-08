using System.Collections.Generic;

public interface IBaseAnimationHandler
{
    void SetAnimationNames(Dictionary<State, string> anims);
    void SetIdleAnim(string idleAnim);
    void SetModel(IMapObjectModel model);
}
