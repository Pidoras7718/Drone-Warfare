using DroneWarfare.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace DroneWarfare.Content.Players;

public sealed class DronePlayer : ModPlayer
{
    public DroneInstance ActiveDrone { get; private set; }

    public int ActiveDroneProjectile { get; private set; } = -1;

    public bool IsControllingDrone { get; private set; }

    public override void Initialize()
    {
        ActiveDrone = null;
        ActiveDroneProjectile = -1;
        IsControllingDrone = false;
    }

    public override void PreUpdate()
    {
        ActiveDrone?.Update(Player);

        if (ActiveDrone is { IsActive: false } || !HasActiveDroneProjectile())
        {
            ActiveDrone = null;
            ActiveDroneProjectile = -1;
            IsControllingDrone = false;
        }
    }

    public override void PreUpdateMovement()
    {
        if (!IsControllingDrone || !HasActiveDroneProjectile())
        {
            return;
        }

        Player.velocity.X = 0f;
        Player.controlLeft = false;
        Player.controlRight = false;
        Player.controlUp = false;
        Player.controlDown = false;
        Player.controlJump = false;
    }

    public override void ModifyScreenPosition()
    {
        if (!IsControllingDrone || !HasActiveDroneProjectile() || Main.myPlayer != Player.whoAmI)
        {
            return;
        }

        Main.screenPosition = Main.projectile[ActiveDroneProjectile].Center - new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (DroneKeys.ToggleDroneControl.JustPressed)
        {
            ToggleDroneControl();
        }

        if (DroneKeys.DetonateDrone.JustPressed)
        {
            DetonateActiveDrone();
        }

        if (DroneKeys.CycleDroneMode.JustPressed)
        {
            CycleDroneMode();
        }
    }

    public void DeployTestDrone(int projectileIndex)
    {
        if (HasActiveDroneProjectile())
        {
            Main.projectile[ActiveDroneProjectile].Kill();
        }

        ActiveDroneProjectile = projectileIndex;
        ActiveDrone = new DroneInstance(Player.whoAmI, Player.Center);
        IsControllingDrone = true;

        if (Main.myPlayer == Player.whoAmI)
        {
            Main.NewText("FPV drone initialized. Drone camera active. Use drone movement binds, F to return to player, V to hover, G to detonate.");
        }
    }

    private bool HasActiveDroneProjectile()
    {
        return ActiveDroneProjectile >= 0
            && ActiveDroneProjectile < Main.maxProjectiles
            && Main.projectile[ActiveDroneProjectile].active
            && Main.projectile[ActiveDroneProjectile].owner == Player.whoAmI;
    }

    private void ToggleDroneControl()
    {
        if (ActiveDrone is null || !HasActiveDroneProjectile())
        {
            return;
        }

        IsControllingDrone = !IsControllingDrone;

        if (Main.myPlayer == Player.whoAmI)
        {
            Main.NewText(IsControllingDrone ? "Drone control enabled. Camera locked to drone." : "Drone control disabled. Drone will keep its current mode.");
        }
    }

    private void DetonateActiveDrone()
    {
        if (ActiveDrone is null || !HasActiveDroneProjectile())
        {
            return;
        }

        Vector2 dronePosition = Main.projectile[ActiveDroneProjectile].Center;
        Main.projectile[ActiveDroneProjectile].Kill();
        ActiveDrone.Deactivate();
        ActiveDrone = null;
        ActiveDroneProjectile = -1;
        IsControllingDrone = false;

        if (Main.myPlayer == Player.whoAmI)
        {
            Main.NewText($"Drone detonated at {dronePosition.X:0}, {dronePosition.Y:0}.");
        }
    }

    private void CycleDroneMode()
    {
        if (ActiveDrone is null || !HasActiveDroneProjectile())
        {
            return;
        }

        ActiveDrone.Mode = ActiveDrone.Mode == DroneMode.Manual ? DroneMode.HoldPosition : DroneMode.Manual;

        if (Main.myPlayer == Player.whoAmI)
        {
            Main.NewText($"Drone mode: {ActiveDrone.Mode}");
        }
    }
}
