using System;
using System.ComponentModel;
using System.Resources;
using DayZTradeCenter.Resources;
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
        [LocalizedDescription("ItemCategories_Weapons", typeof(LocalizedStrings))]
        Weapons,

        [LocalizedDescription("ItemCategories_Medical", typeof(LocalizedStrings))]
        Medical,

        [LocalizedDescription("ItemCategories_Attachments", typeof(LocalizedStrings))]
        Attachments,

        [LocalizedDescription("ItemCategories_Ammunition", typeof(LocalizedStrings))]
        Ammunition,

        [LocalizedDescription("ItemCategories_Magazines", typeof(LocalizedStrings))]
        Magazines,

        [LocalizedDescription("ItemCategories_FoodAndDrink", typeof(LocalizedStrings))]
        FoodAndDrink,

        [LocalizedDescription("ItemCategories_Equipment", typeof(LocalizedStrings))]
        Equipment,

        [LocalizedDescription("ItemCategories_Clothing", typeof(LocalizedStrings))]
        Clothing
    }

    public enum ItemSubcategories
    {
        #region Weapons

        [LocalizedDescription("ItemSubcategories_BluntMelee", typeof (LocalizedStrings))]
        BluntMelee,

        [LocalizedDescription("ItemSubcategories_BladedMelee", typeof (LocalizedStrings))] 
        BladedMelee,

        [LocalizedDescription("ItemSubcategories_ElectricWeapons", typeof (LocalizedStrings))] 
        ElectricWeapons,

        [LocalizedDescription("ItemSubcategories_Bows", typeof (LocalizedStrings))] 
        Bows,

        [LocalizedDescription("ItemSubcategories_Handguns", typeof (LocalizedStrings))] 
        Handguns,

        [LocalizedDescription("ItemSubcategories_Rifles", typeof (LocalizedStrings))] 
        Rifles,

        [LocalizedDescription("ItemSubcategories_SubmachineGuns", typeof (LocalizedStrings))] 
        SubmachineGuns,

        [LocalizedDescription("ItemSubcategories_Shotguns", typeof (LocalizedStrings))] 
        Shotguns,

        [LocalizedDescription("ItemSubcategories_Grenades", typeof (LocalizedStrings))] 
        Grenades,

        #endregion

        #region Medical

        [LocalizedDescription("ItemSubcategories_Bandages", typeof (LocalizedStrings))]
        Bandages,

        [LocalizedDescription("ItemSubcategories_Splints", typeof (LocalizedStrings))] 
        Splints,

        [LocalizedDescription("ItemSubcategories_BloodTransfusion", typeof (LocalizedStrings))] 
        BloodTransfusion,

        [LocalizedDescription("ItemSubcategories_BloodTesting", typeof (LocalizedStrings))] 
        BloodTesting,

        [LocalizedDescription("ItemSubcategories_Disinfection", typeof (LocalizedStrings))] 
        Disinfection,

        [LocalizedDescription("ItemSubcategories_Medicines", typeof (LocalizedStrings))] 
        Medicines,

        [LocalizedDescription("ItemSubcategories_Reanimation", typeof (LocalizedStrings))] 
        Reanimation,

        [LocalizedDescription("ItemSubcategories_Temperature", typeof (LocalizedStrings))] 
        Temperature,

        #endregion

        #region Attachments

        [LocalizedDescription("ItemSubcategories_Buttstocks", typeof(LocalizedStrings))]
        Buttstocks,

        [LocalizedDescription("ItemSubcategories_Handguards", typeof(LocalizedStrings))]
        Handguards,

        [LocalizedDescription("ItemSubcategories_SightsAndOptics", typeof(LocalizedStrings))]
        SightsAndOptics,

        [LocalizedDescription("ItemSubcategories_Illumination", typeof(LocalizedStrings))]
        Illumination,

        [LocalizedDescription("ItemSubcategories_MuzzleAttachments", typeof(LocalizedStrings))]
        MuzzleAttachments,

        [LocalizedDescription("ItemSubcategories_Bayonets", typeof(LocalizedStrings))]
        Bayonets,

        [LocalizedDescription("ItemSubcategories_Bipods", typeof(LocalizedStrings))]
        Bipods,

        [LocalizedDescription("ItemSubcategories_Wraps", typeof(LocalizedStrings))]
        Wraps,

        #endregion

        #region Ammunition

        [LocalizedDescription("ItemSubcategories_HandgunAmmunition", typeof(LocalizedStrings))]
        HandgunAmmunition,

        [LocalizedDescription("ItemSubcategories_ShotgunAmmunition", typeof(LocalizedStrings))]
        ShotgunAmmunition,

        [LocalizedDescription("ItemSubcategories_RifleAmmunition", typeof(LocalizedStrings))]
        RifleAmmunition,

        [LocalizedDescription("ItemSubcategories_MiscellaneousAmmunition", typeof(LocalizedStrings))]
        MiscellaneousAmmunition,

        #endregion

        #region Magazines

        [LocalizedDescription("ItemSubcategories_HandgunMagazines", typeof(LocalizedStrings))]
        HandgunMagazines,

        [LocalizedDescription("ItemSubcategories_RifleMagazines", typeof(LocalizedStrings))]
        RifleMagazines,

        [LocalizedDescription("ItemSubcategories_SubMachineGunMagazines", typeof(LocalizedStrings))]
        SubMachineGunMagazines,

        [LocalizedDescription("ItemSubcategories_Speedloaders", typeof(LocalizedStrings))]
        Speedloaders,

        [LocalizedDescription("ItemSubcategories_MiscellaneousMagazines", typeof(LocalizedStrings))]
        MiscellaneousMagazines,

        #endregion
        
        #region Food and Drink

        [LocalizedDescription("ItemSubcategories_CannedGoods", typeof(LocalizedStrings))]
        CannedGoods,

        [LocalizedDescription("ItemSubcategories_DryFood", typeof(LocalizedStrings))]
        DryFood,

        [LocalizedDescription("ItemSubcategories_Fruits", typeof(LocalizedStrings))]
        Fruits,

        [LocalizedDescription("ItemSubcategories_Vegetables", typeof(LocalizedStrings))]
        Vegetables,

        [LocalizedDescription("ItemSubcategories_Berries", typeof(LocalizedStrings))]
        Berries,

        [LocalizedDescription("ItemSubcategories_Drinks", typeof(LocalizedStrings))]
        Drinks,

        #endregion

        #region Equipment

        [LocalizedDescription("ItemSubcategories_Backpacks", typeof(LocalizedStrings))]
        Backpacks,

        [LocalizedDescription("ItemSubcategories_Containers", typeof(LocalizedStrings))]
        Containers,

        [LocalizedDescription("ItemSubcategories_Cooking", typeof(LocalizedStrings))]
        Cooking,

        [LocalizedDescription("ItemSubcategories_Decorational", typeof(LocalizedStrings))]
        Decorational,

        [LocalizedDescription("ItemSubcategories_Devices", typeof(LocalizedStrings))]
        Devices,

        [LocalizedDescription("ItemSubcategories_LightSources", typeof(LocalizedStrings))]
        LightSources,

        [LocalizedDescription("ItemSubcategories_RepairKits", typeof(LocalizedStrings))]
        RepairKits,

        [LocalizedDescription("ItemSubcategories_Resources", typeof(LocalizedStrings))]
        Resources,

        [LocalizedDescription("ItemSubcategories_Tents", typeof(LocalizedStrings))]
        Tents,

        [LocalizedDescription("ItemSubcategories_Tools", typeof(LocalizedStrings))]
        Tools,

        [LocalizedDescription("ItemSubcategories_Other", typeof(LocalizedStrings))]
        Other,

        #endregion

        #region Clothing

        [LocalizedDescription("ItemSubcategories_Eyewear", typeof(LocalizedStrings))]
        Eyewear,

        [LocalizedDescription("ItemSubcategories_Hats", typeof(LocalizedStrings))]
        Hats,

        [LocalizedDescription("ItemSubcategories_Helmets", typeof(LocalizedStrings))]
        Helmets,

        [LocalizedDescription("ItemSubcategories_Masks", typeof(LocalizedStrings))]
        Masks,

        [LocalizedDescription("ItemSubcategories_HandsAndArms", typeof(LocalizedStrings))]
        HandsAndArms,

        [LocalizedDescription("ItemSubcategories_ShirtsAndJackets", typeof(LocalizedStrings))]
        ShirtsAndJackets,

        [LocalizedDescription("ItemSubcategories_Holsters", typeof(LocalizedStrings))]
        Holsters,

        [LocalizedDescription("ItemSubcategories_Vests", typeof(LocalizedStrings))]
        Vests,

        [LocalizedDescription("ItemSubcategories_Pants", typeof(LocalizedStrings))]
        Pants,

        [LocalizedDescription("ItemSubcategories_Boots", typeof(LocalizedStrings))]
        Boots

        #endregion
    }

    public enum Rarities
    {
        [LocalizedDescription("Rarities_Unknown", typeof(LocalizedStrings))]
        Unknown,

        [LocalizedDescription("Rarities_NA", typeof(LocalizedStrings))]
        NA,

        [LocalizedDescription("Rarities_Common", typeof(LocalizedStrings))]
        Common,

        [LocalizedDescription("Rarities_Uncommon", typeof(LocalizedStrings))]
        Uncommon,

        [LocalizedDescription("Rarities_Rare", typeof(LocalizedStrings))]
        Rare,

        [LocalizedDescription("Rarities_VeryRare", typeof(LocalizedStrings))]
        VeryRare
    }

    /// <summary>
    /// To use localization for enum values.
    /// <see cref="http://stackoverflow.com/questions/17380900/enum-localization"/>
    /// </summary>
    internal class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private readonly ResourceManager _resource;

        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            _resource = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                var displayName = _resource.GetString(_resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", _resourceKey)
                    : displayName;
            }
        }
    }
}
