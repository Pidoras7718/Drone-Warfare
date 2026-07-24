using DroneWarfare.Common;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace DroneWarfare.Content.Players;

public sealed class DronePlayer : ModPlayer
{
    public DroneInstance ActiveDrone { get; private set; }

    public bool IsControllingDrone { get; private set; }

    public override void Initialize()
    {
        ActiveDrone = null;
        IsControllingDrone = false;
    }

    public override void PreUpdate()
    {
        ActiveDrone?.Update(Player);

        if (ActiveDrone is { IsActive: false })
        {
            ActiveDrone = null;
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

    public void DeployTestDrone()
    {
        ActiveDrone = new DroneInstance(Player.whoAmI, Player.Center);
        IsControllingDrone = true;

        if (Main.myPlayer == Player.whoAmI)
        {
            Main.NewText("FPV drone initialized.");
        }
    }

    private void ToggleDroneControl()
    {
        if (ActiveDrone is null)
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
        if (ActiveDrone is null)
        {
            return;
        }

        ActiveDrone.Deactivate();
        ActiveDrone = null;
        IsControllingDrone = false;

        if (Main.myPlayer == Player.whoAmI)
        {
            Main.NewText("Drone detonation placeholder triggered.");
        }
    }

    private void CycleDroneMode()
    {
        if (ActiveDrone is null)
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
