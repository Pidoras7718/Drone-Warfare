using DroneWarfare.Content.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DroneWarfare.Common;

public sealed class DroneHUD : ModSystem
{
    public override void PostDrawInterface(SpriteBatch batch)
    {
        Player player = Main.player[Main.myPlayer];
        if (player is not { active: true, dead: false })
        {
            return;
        }

        DronePlayer dronePlayer = player.GetModPlayer<DronePlayer>();

        if (dronePlayer.ActiveDrone is null || dronePlayer.ActiveDroneProjectile < 0)
        {
            return;
        }

        var drone = dronePlayer.ActiveDrone;
        string droneName = "FPV Drone";
        string fuelText = $"Fuel: {(drone.Fuel * 100f):F0}%";
        string signalText = $"Signal: {(drone.Signal * 100f):F0}%";
        string modeText = $"Mode: {drone.Mode}";
        string payloadText = drone.ActivePayload != null
            ? $"Payload: {drone.ActivePayload.DisplayName}"
            : "Payload: Empty";

        const int x = 20;
        int y = Main.screenHeight - 160;
        const int lineHeight = 22;

        Utils.DrawBorderString(Main.spriteBatch, droneName, new Vector2(x, y), Color.Cyan);
        y += lineHeight;
        Utils.DrawBorderString(Main.spriteBatch, fuelText, new Vector2(x, y), Color.White);
        y += lineHeight;
        Utils.DrawBorderString(Main.spriteBatch, signalText, new Vector2(x, y), Color.White);
        y += lineHeight;
        Utils.DrawBorderString(Main.spriteBatch, modeText, new Vector2(x, y), Color.LightBlue);
        y += lineHeight;
        Utils.DrawBorderString(Main.spriteBatch, payloadText, new Vector2(x, y), drone.ActivePayload != null ? Color.Orange : Color.Gray);
    }
}