using System;

public class StorePlantModel : IStoreObjectModel
{
    public Guid ID { get; set; }
    public int Damage { get; set; }
    public int HP { get; set; }
    public int Type { get; set; }
    public int SkillId { get; set; }

    public bool IsAvaiable { get; set; }
    public int Cost { get; set; }
    public int Countdown { get; set; }
    public float Progress { get; set; }

    public void Parse (PlantConfig config)
    {
        Damage = config.Damage;
        HP = config.HP;
        Type = config.Type;
        SkillId = config.SkillId;
        Cost = config.Cost;
        Countdown = config.Countdown;
        Progress = 0;
    }
}
