using DroneWarfare.Content.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DroneWarfare.Content.Items;

public sealed class FPVController : ModItem
{
    public override string Texture => $"Terraria/Images/Item_{ItemID.MechanicalLens}";

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 24;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item44;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(silver: 50);
        Item.DamageType = DamageClass.Summon;
        Item.noMelee = true;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<DronePlayer>().DeployTestDrone();
        return true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Wire, 10)
            .AddIngredient(ItemID.Lens, 2)
            .AddIngredient(ItemID.IronBar, 5)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Wire, 10)
            .AddIngredient(ItemID.Lens, 2)
            .AddIngredient(ItemID.LeadBar, 5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
