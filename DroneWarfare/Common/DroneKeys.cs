using Terraria.ModLoader;

namespace DroneWarfare.Common;

public sealed class DroneKeys : ModSystem
{
    public static ModKeybind ToggleDroneControl { get; private set; }

    public static ModKeybind DetonateDrone { get; private set; }

    public static ModKeybind CycleDroneMode { get; private set; }

    public override void Load()
    {
        ToggleDroneControl = KeybindLoader.RegisterKeybind(Mod, "ToggleDroneControl", "F");
        DetonateDrone = KeybindLoader.RegisterKeybind(Mod, "DetonateDrone", "G");
        CycleDroneMode = KeybindLoader.RegisterKeybind(Mod, "CycleDroneMode", "V");
    }

    public override void Unload()
    {
        ToggleDroneControl = null;
        DetonateDrone = null;
        CycleDroneMode = null;
    }
}
