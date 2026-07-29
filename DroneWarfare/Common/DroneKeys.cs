using Terraria.ModLoader;

namespace DroneWarfare.Common;

public sealed class DroneKeys : ModSystem
{
    public static ModKeybind ToggleDroneControl { get; private set; }

    public static ModKeybind DetonateDrone { get; private set; }

    public static ModKeybind CycleDroneMode { get; private set; }

    public static ModKeybind DroneMoveUp { get; private set; }

    public static ModKeybind DroneMoveDown { get; private set; }

    public static ModKeybind DroneMoveLeft { get; private set; }

    public static ModKeybind DroneMoveRight { get; private set; }

    public override void Load()
    {
        ToggleDroneControl = KeybindLoader.RegisterKeybind(Mod, "ToggleDroneControl", "F");
        DetonateDrone = KeybindLoader.RegisterKeybind(Mod, "DetonateDrone", "G");
        CycleDroneMode = KeybindLoader.RegisterKeybind(Mod, "CycleDroneMode", "V");
        DroneMoveUp = KeybindLoader.RegisterKeybind(Mod, "DroneMoveUp", "W");
        DroneMoveDown = KeybindLoader.RegisterKeybind(Mod, "DroneMoveDown", "S");
        DroneMoveLeft = KeybindLoader.RegisterKeybind(Mod, "DroneMoveLeft", "A");
        DroneMoveRight = KeybindLoader.RegisterKeybind(Mod, "DroneMoveRight", "D");
    }

    public override void Unload()
    {
        ToggleDroneControl = null;
        DetonateDrone = null;
        CycleDroneMode = null;
        DroneMoveUp = null;
        DroneMoveDown = null;
        DroneMoveLeft = null;
        DroneMoveRight = null;
    }
}
