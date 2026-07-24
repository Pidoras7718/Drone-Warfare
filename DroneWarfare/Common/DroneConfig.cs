using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DroneWarfare.Common;

public sealed class DroneConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool ShowOperatorHud { get; set; }

    [DefaultValue(true)]
    public bool ShowDebugMessages { get; set; }

    [Range(0.1f, 3f)]
    [DefaultValue(1f)]
    public float HudScale { get; set; }
}
