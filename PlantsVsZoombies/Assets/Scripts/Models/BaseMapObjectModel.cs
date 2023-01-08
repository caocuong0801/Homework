using System;
using UnityEngine;

public abstract class BaseMapObjectModel : IMapObjectModel
{
    public Guid ID { get; set; }
    public int Damage { get; set; }
    public int HP { get; set; }
    public int Type { get; set; }
    public int SkillId { get; set; }

    public State State { get; set; }
    public int HurtLevel { get; set; }
    public Vector2 Position { get; set; }
    public event Action<State> OnStateChanged;
}
