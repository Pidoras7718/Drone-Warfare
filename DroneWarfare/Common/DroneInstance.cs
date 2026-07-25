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

    /// <summary>
    /// Returns the payload descriptor based on the currently loaded payload item type.
    /// Returns null if no payload is loaded (PayloadItemType == 0).
    /// </summary>
    public DronePayload? ActivePayload => PayloadItemType == 0 ? null : DronePayload.GetPayload(PayloadItemType) ?? DronePayload.DefaultPayload;

    /// <summary>
    /// Tries to load a payload from the owner player's inventory.
    /// Only the first stack of matching ammo is consumed.
    /// Returns true if a payload was loaded, false if no suitable ammo was found.
    /// </summary>
    public bool TryLoadPayloadFromInventory(Player owner)
    {
        for (int i = 0; i < 58; i++)
        {
            Item item = owner.inventory[i];
            if (item.IsAir || item.stack <= 0)
            {
                continue;
            }

            DronePayload? payload = DronePayload.GetPayload(item.type);
            if (payload != null)
            {
                PayloadItemType = item.type;
                item.stack--;

                if (item.stack <= 0)
                {
                    item.TurnToAir();
                }

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Clears the currently loaded payload (sets PayloadItemType to 0).
    /// </summary>
    public void ClearPayload()
    {
        PayloadItemType = 0;
    }

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
