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
            Main.NewText("FPV drone initialized. Use WASD to fly, F to toggle control, G to detonate.");
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
            Main.NewText(IsControllingDrone ? "Drone control enabled." : "Drone control disabled.");
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

        ActiveDrone.Mode = ActiveDrone.Mode == DroneMode.Manual ? DroneMode.EmergencyManual : DroneMode.Manual;

        if (Main.myPlayer == Player.whoAmI)
        {
            Main.NewText($"Drone mode: {ActiveDrone.Mode}");
        }
    }
}
