using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DroneWarfare.Common;

/// <summary>
/// Defines explosion parameters for a drone payload based on the ammo item type.
/// </summary>
public sealed class DronePayload
{
    /// <summary>
    /// The item type of the ammunition used as payload.
    /// </summary>
    public int ItemType { get; }

    /// <summary>
    /// Display name shown in HUD.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Base explosion damage dealt to NPCs and players.
    /// </summary>
    public int ExplosionDamage { get; }

    /// <summary>
    /// Radius of the explosion in pixels.
    /// </summary>
    public int ExplosionSize { get; }

    /// <summary>
    /// Type of dust used for the explosion visual effect.
    /// </summary>
    public int DustType { get; }

    /// <summary>
    /// Number of dust particles spawned on explosion.
    /// </summary>
    public int DustCount { get; }

    private DronePayload(int itemType, string displayName, int explosionDamage, int explosionSize, int dustType, int dustCount)
    {
        ItemType = itemType;
        DisplayName = displayName;
        ExplosionDamage = explosionDamage;
        ExplosionSize = explosionSize;
        DustType = dustType;
        DustCount = dustCount;
    }

    /// <summary>
    /// Returns the payload definition for a given item type, or null if the item is not a valid payload.
    /// </summary>
    public static DronePayload? GetPayload(int itemType)
    {
        return itemType switch
        {
            ItemID.ExplodingBullet => new DronePayload(itemType, "Explosive Rounds", 60, 100, DustID.Smoke, 20),
            ItemID.RocketI         => new DronePayload(itemType, "Rocket I", 100, 160, DustID.Torch, 28),
            ItemID.RocketII        => new DronePayload(itemType, "Rocket II", 100, 160, DustID.Torch, 28),
            ItemID.RocketIII       => new DronePayload(itemType, "Rocket III", 130, 180, DustID.InfernoFork, 35),
            ItemID.RocketIV        => new DronePayload(itemType, "Rocket IV", 130, 180, DustID.InfernoFork, 35),
            ItemID.Grenade         => new DronePayload(itemType, "Grenade", 70, 120, DustID.Smoke, 22),
            ItemID.Bomb            => new DronePayload(itemType, "Bomb", 80, 140, DustID.Torch, 25),
            ItemID.Dynamite        => new DronePayload(itemType, "Dynamite", 120, 200, DustID.InfernoFork, 40),
            ItemID.ExplosiveJackOLantern => new DronePayload(itemType, "Jack'O Lantern", 90, 150, DustID.Pumpkin, 28),
            _ => null
        };
    }

    /// <summary>
    /// The default / fallback payload used when ammo is loaded but no specific mapping exists.
    /// </summary>
    public static DronePayload DefaultPayload => new(0, "Default Charge", 80, 160, DustID.Torch, 28);
}