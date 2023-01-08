using System;

public interface IPlantHandler
{
    IBaseAnimationHandler AnimationHandler { get; }

    Guid Id { get; set; }
    void SetModel(MapPlantModel model);
}
