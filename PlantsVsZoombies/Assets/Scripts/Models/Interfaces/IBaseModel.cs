using System;

public interface IBaseModel
{
    Guid ID { get; set; }
    int Damage { get; set; }
    int HP { get; set; }
    int Type { get; set; }
    int SkillId { get; set; }
}
