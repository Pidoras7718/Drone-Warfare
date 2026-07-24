using Microsoft.Xna.Framework;
using Terraria;

namespace DroneWarfare.Common;

public sealed class DroneInstance
{
    public DroneInstance(int owner, Vector2 position)
    {
        Owner = owner;
        Position = position;
        Velocity = Vector2.Zero;
        Rotation = 0f;
        Throttle = 0f;
        Fuel = 1f;
        Signal = 1f;
        PayloadItemType = 0;
        Mode = DroneMode.Manual;
        TargetWhoAmI = -1;
        Health = 1f;
    }

    public int Owner { get; }

    public Vector2 Position { get; set; }

    public Vector2 Velocity { get; set; }

    public float Rotation { get; set; }

    public float Throttle { get; set; }

    public float Fuel { get; set; }

    public float Signal { get; set; }

    public int PayloadItemType { get; set; }

    public DroneMode Mode { get; set; }

    public int TargetWhoAmI { get; set; }

    public float Health { get; set; }

    public bool IsActive => Mode != DroneMode.Inactive && Health > 0f;

    public void Deactivate()
    {
        Mode = DroneMode.Inactive;
        Velocity = Vector2.Zero;
        Throttle = 0f;
    }

    public void Update(Player owner)
    {
        if (!IsActive)
        {
            return;
        }

        Position += Velocity;

        if (Velocity.LengthSquared() > 0.001f)
        {
            Rotation = Velocity.ToRotation();
        }

        Fuel = MathHelper.Clamp(Fuel - 0.0005f, 0f, 1f);

        if (Fuel <= 0f || !owner.active || owner.dead)
        {
            Deactivate();
        }
    }
}
