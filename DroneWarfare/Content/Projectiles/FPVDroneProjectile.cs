using DroneWarfare.Common;
using DroneWarfare.Content.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DroneWarfare.Content.Projectiles;

public sealed class FPVDroneProjectile : ModProjectile
{
    private const float Acceleration = 0.22f;
    private const float MaxSpeed = 8f;
    private const float Drag = 0.96f;
    private const float HoldDrag = 0.82f;

    public override string Texture => $"Terraria/Images/Item_{ItemID.MechanicalLens}";

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 1;
    }

    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.friendly = false;
        Projectile.hostile = false;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 60 * 60;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.netImportant = true;
    }

    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];

        if (!owner.active || owner.dead)
        {
            Projectile.Kill();
            return;
        }

        DronePlayer dronePlayer = owner.GetModPlayer<DronePlayer>();

        if (dronePlayer.ActiveDrone?.Mode == DroneMode.HoldPosition)
        {
            Projectile.velocity *= HoldDrag;
        }
        else if (Main.myPlayer == Projectile.owner && dronePlayer.IsControllingDrone)
        {
            Vector2 input = GetDroneInput();
            Projectile.velocity += input * Acceleration;

            if (Projectile.velocity.Length() > MaxSpeed)
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * MaxSpeed;
            }

            Projectile.netUpdate = true;
        }

        Projectile.velocity *= Drag;

        if (Projectile.velocity.LengthSquared() > 0.05f)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        Lighting.AddLight(Projectile.Center, 0.2f, 0.45f, 0.55f);
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.Kill();
        return false;
    }

    public override void Kill(int timeLeft)
    {
        Explode();
    }

    private static Vector2 GetDroneInput()
    {
        Vector2 input = Vector2.Zero;

        if (DroneKeys.DroneMoveLeft.Current)
        {
            input.X -= 1f;
        }

        if (DroneKeys.DroneMoveRight.Current)
        {
            input.X += 1f;
        }

        if (DroneKeys.DroneMoveUp.Current)
        {
            input.Y -= 1f;
        }

        if (DroneKeys.DroneMoveDown.Current)
        {
            input.Y += 1f;
        }

        if (input.LengthSquared() > 1f)
        {
            input.Normalize();
        }

        return input;
    }

    private void Explode()
    {
        Player owner = Main.player[Projectile.owner];
        DronePlayer dronePlayer = owner.GetModPlayer<DronePlayer>();
        DronePayload? payload = dronePlayer.ActiveDrone?.ActivePayload;

        // No payload loaded — drone silently breaks apart, no explosion.
        if (payload is null)
        {
            return;
        }

        Vector2 oldCenter = Projectile.Center;
        int explosionSize = payload.ExplosionSize;
        int explosionDamage = payload.ExplosionDamage;

        Projectile.position = oldCenter - new Vector2(explosionSize / 2f);
        Projectile.width = explosionSize;
        Projectile.height = explosionSize;
        Projectile.damage = explosionDamage;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.Damage();

        DroneDamageSystem.ApplyExplosionDamageToPlayers(oldCenter, explosionDamage, explosionSize / 2f);

        for (int i = 0; i < payload.DustCount; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, payload.DustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
        }

        if (Main.myPlayer == Projectile.owner)
        {
            DestroyLooseTiles(oldCenter);
        }

        SoundEngine.PlaySound(SoundID.Item14, oldCenter);
    }

    private static void DestroyLooseTiles(Vector2 center)
    {
        int tileRadius = 2;
        Point tileCenter = center.ToTileCoordinates();

        for (int x = tileCenter.X - tileRadius; x <= tileCenter.X + tileRadius; x++)
        {
            for (int y = tileCenter.Y - tileRadius; y <= tileCenter.Y + tileRadius; y++)
            {
                if (!WorldGen.InWorld(x, y))
                {
                    continue;
                }

                Tile tile = Framing.GetTileSafely(x, y);

                if (!tile.HasTile || Main.tileDungeon[tile.TileType] || Main.tileFrameImportant[tile.TileType])
                {
                    continue;
                }

                WorldGen.KillTile(x, y, fail: false, effectOnly: false, noItem: false);
            }
        }
    }
}