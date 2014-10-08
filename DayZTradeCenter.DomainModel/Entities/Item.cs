using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel.Entities
{
    /// <summary>
    /// A DayZ game item.
    /// </summary>
    public class Item : IEntity
    {
        #region Ctors

        public Item(string name, CategoryInfo details)
        {
            Name = name;
            Details = details;
        }

        public Item(string name, CategoryInfo details, Rarities rarity)
        {
            Name = name;
            Details = details;
            Rarity = rarity;
        }

        public Item()
        {
        }

        #endregion
        
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryInfo Details { get; set; }
        public Rarities Rarity { get; set; }
    }

    public class CategoryInfo
    {
        public ItemCategories Category { get; set; }
        public ItemSubcategories Subcategory { get; set; }
    }

    public enum ItemCategories
    {
        Weapons,
        Medical,
        Attachments,
        Ammunition,
        Magazines,
        FoodAndDrink,
        Equipment,
        Clothing
    }

    public enum ItemSubcategories
    {
        // Weapons
        BluntMelee,
        BladedMelee,
        ElectricWeapons,
        Bows,
        Handguns,
        Rifles,
        SubmachineGuns,
        Shotguns,
        Grenades,

        // Medical
        Bandages,
        Splints,
        BloodTransfusion,
        BloodTesting,
        Disinfection,
        Medicines,
        Reanimation,
        Temperature,

        // Attachments
        Buttstocks,
        Handguards,
        SightsAndOptics,
        Illumination,
        MuzzleAttachments,
        Bayonets,
        Bipods,
        Wraps,

        // Ammunition
        HandgunAmmunition,
        ShotgunAmmunition,
        RifleAmmunition,
        MiscellaneousAmmunition,

        // Magazines
        HandgunMagazines,
        RifleMagazines,
        SubMachineGunMagazines,
        Speedloaders,
        MiscellaneousMagazines,

        // Food and Drink
        CannedGoods,
        DryFood,
        Fruits,
        Vegetables,
        Berries,
        Drinks,

        // Equipment
        Backpacks,
        Containers,
        Cooking,
        Decorational,
        Devices,
        LightSources,
        RepairKits,
        Resources,
        Tents,
        Tools,
        Other,

        // Clothing
        Eyewear,
        Hats,
        Helmets,
        Masks,
        HandsAndArms,
        ShirtsAndJackets,
        Holsters,
        Vests,
        Pants,
        Boots
    }

    public enum Rarities
    {
        Unknown,
        NA,
        Common,
        Uncommon, 
        Rare,
        VeryRare
    }
}
