using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlantModel : BaseMapObjectModel
{
    public void Parse(IStoreObjectModel model)
    {
        ID = model.ID;
        Damage = model.Damage;
        HP = model.HP;
        Type = model.Type;
        SkillId = model.SkillId;
    }
}
