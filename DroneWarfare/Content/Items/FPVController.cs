using DroneWarfare.Content.Players;
using DroneWarfare.Content.Projectiles;
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
        if (player.whoAmI != Main.myPlayer)
        {
            return true;
        }

        int projectileType = ModContent.ProjectileType<FPVDroneProjectile>();
        int projectileIndex = Projectile.NewProjectile(
            player.GetSource_ItemUse(Item),
            player.Center + new Microsoft.Xna.Framework.Vector2(player.direction * 48f, -24f),
            new Microsoft.Xna.Framework.Vector2(player.direction * 2f, -1f),
            projectileType,
            10,
            1f,
            player.whoAmI);

        player.GetModPlayer<DronePlayer>().DeployTestDrone(projectileIndex);
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
