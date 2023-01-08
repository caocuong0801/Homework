using UnityEngine;

public class BulletModel : BaseMapObjectModel
{
    public float Speed { get; set; }
    public bool IsCurveMoving { get; set; }
    public Vector2 Target { get; set; }
}
