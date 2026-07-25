using Microsoft.Xna.Framework;
using Terraria;

namespace DroneWarfare.Common;

public static class DroneDamageSystem
{
    /// <summary>
    /// Applies damage to all players (including the drone owner) within the specified radius,
    /// bypassing PvP checks. This is called when a drone explodes.
    /// </summary>
    /// <param name="center">The center of the explosion in world coordinates.</param>
    /// <param name="damage">The amount of damage to apply.</param>
    /// <param name="radius">The radius in pixels within which players take damage.</param>
    public static void ApplyExplosionDamageToPlayers(Vector2 center, int damage, float radius)
    {
        float radiusSq = radius * radius;

        for (int i = 0; i < Main.maxPlayers; i++)
        {
            Player player = Main.player[i];

            if (!player.active || player.dead)
            {
                continue;
            }

            float distSq = Vector2.DistanceSquared(player.Center, center);

            if (distSq > radiusSq)
            {
                continue;
            }

            int direction = player.Center.X > center.X ? 1 : -1;
            player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was caught in a drone explosion."), damage, direction);
        }
    }
}