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

    public override string Texture => $"Terraria/Images/Item_{ItemID.MechanicalLens}";

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 1;
    }

    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.tileCollide = false;
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

        if (Main.myPlayer == Projectile.owner && dronePlayer.IsControllingDrone)
        {
            Vector2 input = Vector2.Zero;

            if (owner.controlLeft)
            {
                input.X -= 1f;
            }

            if (owner.controlRight)
            {
                input.X += 1f;
            }

            if (owner.controlUp || owner.controlJump)
            {
                input.Y -= 1f;
            }

            if (owner.controlDown)
            {
                input.Y += 1f;
            }

            if (input.LengthSquared() > 1f)
            {
                input.Normalize();
            }

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

    public override void Kill(int timeLeft)
    {
        for (int i = 0; i < 12; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
        }

        SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
    }
}
