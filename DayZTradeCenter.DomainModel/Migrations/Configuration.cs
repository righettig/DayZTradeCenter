using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
#if DEBUG
            AutomaticMigrationsEnabled = false;
#else
            // http://blog.appharbor.com/2012/04/24/automatic-migrations-with-entity-framework-4-3
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
#endif
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Uncomment to debug code-first migrations code
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            CreateRoles(context);
            CreateUsers(context);

            #region Items

            // Last update: 08/10/2014

            #region Weapons

            // http://dayz.gamepedia.com/Weapons
            const ItemCategories weapons = ItemCategories.Weapons;

            // Melee Weapons
            //-------------------

            //  Blunt weapons
            CreateItem(context, weapons, ItemSubcategories.BluntMelee,
                new[]
                {
                    new ItemInfo("Baseball bat", Rarities.Uncommon),
                    new ItemInfo("Crowbar", Rarities.Uncommon),
                    new ItemInfo("Fire Extinguisher", Rarities.Uncommon),
                    new ItemInfo("Pipe Wrench", Rarities.Rare),
                    new ItemInfo("Telescopic Baton", Rarities.Uncommon)
                });

            //  Bladed weapons
            CreateItem(context, weapons, ItemSubcategories.BladedMelee,
                new[]
                {
                    new ItemInfo("Combat Knife", Rarities.VeryRare),
                    new ItemInfo("Machete", Rarities.Uncommon),
                    new ItemInfo("Sickle", Rarities.Uncommon),
                    new ItemInfo("Pitchfork", Rarities.Uncommon)
                });

            //  Electroshock Weapons
            CreateItem(context, weapons, ItemSubcategories.ElectricWeapons,
                new[]
                {
                    new ItemInfo("Electric Cattle Prod", Rarities.Common),
                    new ItemInfo("Stun Baton", Rarities.Uncommon)
                });


            // Bows
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Bows,
                new[]
                {
                    new ItemInfo("Crossbow", Rarities.Common),
                    new ItemInfo("Improvised Ashwood Short Bow", Rarities.NA)
                });

            
            // Handguns
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Handguns,
                new[]
                {
                    new ItemInfo("Amphibia S", Rarities.Uncommon),
                    new ItemInfo("Makarov IJ70", Rarities.Rare),
                    new ItemInfo("P1", Rarities.Rare),
                    new ItemInfo("CR75", Rarities.Rare),
                    new ItemInfo("FNX45", Rarities.Uncommon),
                    new ItemInfo("1911", Rarities.Uncommon),
                    new ItemInfo("1911 Engraved", Rarities.VeryRare),
                    new ItemInfo("Magnum", Rarities.Uncommon),
                    new ItemInfo("LongHorn", Rarities.VeryRare)
                });


            // Rifles
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Rifles,
                new[]
                {
                    new ItemInfo("Sporter 22", Rarities.Common),
                    new ItemInfo("AK101", Rarities.Uncommon),
                    new ItemInfo("M4A1", Rarities.VeryRare),
                    new ItemInfo("CR527 Carbine", Rarities.Uncommon),
                    new ItemInfo("SKS", Rarities.Common),
                    new ItemInfo("AKM", Rarities.Uncommon),
                    new ItemInfo("Blaze 95 Double Rifle", Rarities.Rare),
                    new ItemInfo("Mosin 9130", Rarities.Uncommon),
                    new ItemInfo("Sawed-off Mosin 9130", Rarities.NA)
                });


            // Submachine - Guns
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.SubmachineGuns,
                new[]
                {
                    new ItemInfo("PM73 RAK", Rarities.VeryRare),
                    new ItemInfo("MP5-K", Rarities.Rare)
                });


            // Shotguns
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.SubmachineGuns,
                new[]
                {
                    new ItemInfo("IZH-43", Rarities.Uncommon),
                    new ItemInfo("Sawed-off IZH-43", Rarities.NA)
                });


            // Grenades
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Grenades,
                new[]
                {
                    new ItemInfo("Explosive Grenade", Rarities.VeryRare),
                    new ItemInfo("Flashbang", Rarities.Uncommon),
                    new ItemInfo("RDG-5 Explosive Grenade", Rarities.VeryRare)                   
                });
            
            #endregion

            #region Medical Supplies

            // http://dayz.gamepedia.com/Medical_Supplies

            const ItemCategories medicalSupplies = ItemCategories.Medical;

            // Bandages
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Bandages,
                new[]
                {
                    new ItemInfo("Bandage", Rarities.Uncommon),
                    new ItemInfo("Rags", Rarities.Uncommon)
                });


            // Splints
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Splints,
                new[]
                {
                    new ItemInfo("Wooden Splint", Rarities.NA)
                });


            // Blood Transfusion
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.BloodTransfusion,
                new[]
                {
                    new ItemInfo("Blood Bag", Rarities.Uncommon),
                    new ItemInfo("Blood Bag Kit", Rarities.Uncommon),
                    new ItemInfo("Saline Bag", Rarities.Common),
                    new ItemInfo("Saline Bag IV", Rarities.Rare),
                    new ItemInfo("IV Start Kit", Rarities.Uncommon)
                });


            // Blood Testing
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.BloodTesting,
                new[]
                {
                    new ItemInfo("Blood Testing Kit", Rarities.Rare),
                    new ItemInfo("Syringe", Rarities.Uncommon)
                });


            // Disinfection
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Disinfection,
                new[]
                {
                    new ItemInfo("Alcohol Tincture", Rarities.Uncommon),
                    new ItemInfo("Disinfectant Spray", Rarities.Uncommon),
                    new ItemInfo("Water Purification Tablets", Rarities.Uncommon)
                });


            // Medicines
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Medicines,
                new[]
                {
                    new ItemInfo("Charcoal Tabs", Rarities.Rare),
                    new ItemInfo("Injection Vial", Rarities.Uncommon),
                    new ItemInfo("Morphine Auto-Injector", Rarities.Rare),
                    new ItemInfo("Painkillers", Rarities.Uncommon),
                    new ItemInfo("Tetracycline Antibiotics", Rarities.Common),
                    new ItemInfo("Vitamin Bottles", Rarities.Common)
                });


            // Reanimation
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Reanimation,
                new[]
                {
                    new ItemInfo("Defibrillator", Rarities.Uncommon),
                    new ItemInfo("Epinephrine", Rarities.Rare)
                });


            // Temperature
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Temperature,
                new[]
                {
                    new ItemInfo("Medical Thermometer", Rarities.Unknown) // unknown 08/10/14
                });

            #endregion

            #region Attachments

            // http://dayz.gamepedia.com/Attachments

            const ItemCategories attachments = ItemCategories.Attachments;

            // Buttstocks
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Buttstocks,
                new[]
                {
                    new ItemInfo("AK Folding Buttstock", Rarities.Rare),
                    new ItemInfo("AK Plastic Buttstock", Rarities.Unknown),
                    new ItemInfo("AK Wooden Buttstock", Rarities.Rare),
                    new ItemInfo("M4 Buttstock CQB", Rarities.Uncommon),
                    new ItemInfo("M4 Buttstock OE", Rarities.Uncommon),
                    new ItemInfo("M4 Buttstock MP", Rarities.Rare),
                    new ItemInfo("MP5 OEM Buttstock", Rarities.Rare)
                });


            // Handguards
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Handguards,
                new[]
                {
                    new ItemInfo("AK Handguard Rail", Rarities.Rare),
                    new ItemInfo("AK Handguard Plastic", Rarities.Rare),
                    new ItemInfo("AK Wooden Handguard", Rarities.Rare),
                    new ItemInfo("M4 Handguard MP", Rarities.Rare),
                    new ItemInfo("M4 Handguard Plastic", Rarities.Uncommon),
                    new ItemInfo("M4 Handguard RIS", Rarities.Rare),
                    new ItemInfo("MP5 Plastic Handguard", Rarities.Rare)                   
                });


            // Sights and Optics
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.SightsAndOptics,
                new[]
                {
                     new ItemInfo("ACOG Optics", Rarities.Rare),
                     new ItemInfo("BUIS", Rarities.Rare),
                     new ItemInfo("Crossbow Holosight", Rarities.Rare),
                     new ItemInfo("FNP45 MRD", Rarities.Rare),
                     new ItemInfo("Long Range Scope", Rarities.Uncommon),
                     new ItemInfo("M4 Carryhandle Optics", Rarities.Rare),
                     new ItemInfo("M68 CompM2 Optics", Rarities.Rare),
                     new ItemInfo("PSO1 Scope", Rarities.Rare),
                     new ItemInfo("PU Scope", Rarities.Uncommon),
                     new ItemInfo("RV1 RDS Optics", Rarities.Rare),
                     new ItemInfo("Crossbow Scope", Rarities.VeryRare)
                });


            // Illumination
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Illumination,
                new[]
                {
                    new ItemInfo("Pistol Flashlight", Rarities.Rare),
                    new ItemInfo("Weapon Flashlight", Rarities.Rare)
                });


            // Muzzle Attachments
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.MuzzleAttachments,
                new[]
                {
                    new ItemInfo("Mosin M44 Compensator", Rarities.Rare),
                    new ItemInfo("Pistol Suppressor", Rarities.Rare),
                    new ItemInfo("Suppressor 556", Rarities.Rare),
                    new ItemInfo("Suppressor 5.45x39mm", Rarities.Rare)
                });


            // Bayonets
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Bayonets,
                new[]
                {
                    new ItemInfo("M9A1 Bayonet", Rarities.Rare),
                    new ItemInfo("M91 Bayonet", Rarities.Uncommon),
                    new ItemInfo("SKS Bayonet", Rarities.Uncommon)
                });


            // Bipods
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Bipods,
                new[]
                {
                    new ItemInfo("ATLAS Bipod", Rarities.Uncommon)
                });


            // Wraps
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Wraps,
                new[]
                {
                    new ItemInfo("Burlap Wrap", Rarities.NA),
                    new ItemInfo("Grass Wrap", Rarities.NA)
                });

            #endregion

            #region Ammunition

            // http://dayz.gamepedia.com/Ammunition

            const ItemCategories ammunition = ItemCategories.Ammunition;

            // Handgun Ammunition
            //-------------------
            CreateItem(context, ammunition, ItemSubcategories.HandgunAmmunition,
                new[]
                {
                    new ItemInfo(".22 Round(s)", Rarities.Uncommon),
                    new ItemInfo(".380 Auto Round(s)", Rarities.Uncommon),
                    new ItemInfo("9mm Round(s)", Rarities.Common),
                    new ItemInfo(".45ACP Round(s)", Rarities.Uncommon),
                    new ItemInfo(".357 Round(s)", Rarities.Rare)
                });


            // Shotgun Ammunition
            //-------------------
            CreateItem(context, ammunition, ItemSubcategories.ShotgunAmmunition,
                new[]
                {
                    new ItemInfo("12 Gauge Buckshot", Rarities.Common)
                });


            // Rifle Ammunition
            //-------------------
            CreateItem(context, ammunition, ItemSubcategories.RifleAmmunition,
                new[]
                {
                    new ItemInfo("7.62x39mm Round(s)", Rarities.Uncommon),
                    new ItemInfo("5.56mm Round(s)", Rarities.Uncommon),
                    new ItemInfo("7.62mm Round(s)", Rarities.Uncommon)
                });


            // Miscellaneous Ammunition
            //-------------------
            CreateItem(context, ammunition, ItemSubcategories.MiscellaneousAmmunition,
                new[]
                {
                    new ItemInfo("Crossbow bolt(s)", Rarities.Uncommon),
                    new ItemInfo("Composite Arrow", Rarities.Rare),
                    new ItemInfo("Improvised Arrow", Rarities.NA)
                });

            #endregion

            #region Magazines

            // http://dayz.gamepedia.com/Magazines

            const ItemCategories magazines = ItemCategories.Magazines;

            // Handgun Magazines
            //-------------------
            CreateItem(context, magazines, ItemSubcategories.HandgunMagazines,
                new[]
                {
                    new ItemInfo("7Rnd 1911 Magazine", Rarities.Uncommon),
                    new ItemInfo("Makarov magazine", Rarities.Rare),
                    new ItemInfo("8Rnd P1 Magazine", Rarities.Uncommon),
                    new ItemInfo("10Rnd .22 Mag pistol", Rarities.Rare),
                    new ItemInfo("15Rnd CR75 Magazine", Rarities.Rare),
                    new ItemInfo("15Rnd FNX45 Magazine", Rarities.Rare)
                });


            // Rifle Magazines
            //-------------------
            CreateItem(context, magazines, ItemSubcategories.RifleMagazines,
                new[]
                {
                    new ItemInfo("10Rnd .22 Mag", Rarities.Uncommon),
                    new ItemInfo("30Rnd .22 Mag", Rarities.Uncommon),
                    new ItemInfo("10Rnd 5.56mm CMAG", Rarities.Rare),
                    new ItemInfo("20Rnd 5.56mm CMAG", Rarities.Unknown),
                    new ItemInfo("30Rnd 5.56mm CMAG", Rarities.Unknown),
                    new ItemInfo("40Rnd 5.56mm CMAG", Rarities.Unknown),
                    new ItemInfo("30Rnd STANAG", Rarities.Unknown),
                    new ItemInfo("30Rnd STANAG (coupled)", Rarities.Unknown),
                    new ItemInfo("30Rnd AK101 mag", Rarities.Rare),
                    new ItemInfo("30rnd Mag", Rarities.Common),
                    new ItemInfo("75rnd Mag", Rarities.VeryRare),
                    new ItemInfo("CZ527 Magazine", Rarities.Uncommon)
                });

            // Sub-Machine Gun Magazines
            //-------------------
            CreateItem(context, magazines, ItemSubcategories.SubMachineGunMagazines,
                new[]
                {
                    new ItemInfo("15rnd PM73 mag", Rarities.VeryRare),
                    new ItemInfo("25rnd PM73 mag", Rarities.VeryRare),
                    new ItemInfo("15Rnd MP5 Magazine", Rarities.Rare),
                    new ItemInfo("30Rnd MP5 Magazine", Rarities.Rare)
                });


            // Speedloaders
            //-------------------
            CreateItem(context, magazines, ItemSubcategories.Speedloaders,
                new[]
                {
                    new ItemInfo("5Rnd Clip", Rarities.Unknown),
                    new ItemInfo("10 Round Clip", Rarities.Rare),
                    new ItemInfo("12 Ga Pellet Snaploader", Rarities.Rare),
                    new ItemInfo(".357 Speedloader", Rarities.Uncommon),
                    new ItemInfo("Snaploader", Rarities.Uncommon)
                });


            // Miscellaneous Magazines
            //-------------------
            CreateItem(context, magazines, ItemSubcategories.MiscellaneousMagazines,
                new[]
                {
                    new ItemInfo("Bolts Quiver", Rarities.Rare)
                });

            #endregion

            #region Food and Drink

            // http://dayz.gamepedia.com/Food_and_Drink

            const ItemCategories foodAndDrink = ItemCategories.FoodAndDrink;

            // Canned Goods
            //-------------------
            CreateItem(context, foodAndDrink, ItemSubcategories.CannedGoods,
                new[]
                {
                    new ItemInfo("Canned Baked Beans", Rarities.Common),
                    new ItemInfo("Canned Sardines", Rarities.Common),
                    new ItemInfo("Canned Spaghetti", Rarities.Common),
                    new ItemInfo("Canned Tuna", Rarities.Common),
                    new ItemInfo("Can of Tactical Bacon", Rarities.Common),
                    new ItemInfo("Canned Peaches", Rarities.Common)
                });


            // Dry Food
            //-------------------
            CreateItem(context, foodAndDrink, ItemSubcategories.DryFood,
                new[]
                {
                    new ItemInfo("Powdered Milk", Rarities.Uncommon),
                    new ItemInfo("Rice", Rarities.Uncommon),
                    new ItemInfo("Box of Cereal", Rarities.Rare)
                });


            // Fruits
            //-------------------
            CreateItem(context, foodAndDrink, ItemSubcategories.Fruits,
                new[]
                {
                    new ItemInfo("Apple", Rarities.Rare),
                    new ItemInfo("Banana", Rarities.Uncommon),
                    new ItemInfo("Kiwi", Rarities.Rare),
                    new ItemInfo("Orange", Rarities.Uncommon),
                    new ItemInfo("Tomato", Rarities.Rare),
                    new ItemInfo("Rotten Apple", Rarities.Common),
                    new ItemInfo("Rotten Banana", Rarities.Common),
                    new ItemInfo("Rotten Kiwi", Rarities.Common),
                    new ItemInfo("Rotten Orange", Rarities.Common),
                    new ItemInfo("Rotten Tomato", Rarities.Uncommon)
                });


            // Vegetables
            //-------------------
            CreateItem(context, foodAndDrink, ItemSubcategories.Vegetables,
                new[]
                {
                    new ItemInfo("Green Pepper", Rarities.Rare),
                    new ItemInfo("Potato", Rarities.Uncommon),
                    new ItemInfo("Zucchini", Rarities.Rare),
                    new ItemInfo("Rotten Green Pepper", Rarities.Uncommon),
                    new ItemInfo("Rotten Potato", Rarities.Common),
                    new ItemInfo("Rotten Zucchini", Rarities.Common)
                });


            // Berries
            //-------------------
            CreateItem(context, foodAndDrink, ItemSubcategories.Berries,
                new[]
                {
                    new ItemInfo("Red Berries", Rarities.NA),
                    new ItemInfo("Blue Berries", Rarities.NA)
                });


            // Drinks
            //-------------------
            CreateItem(context, foodAndDrink, ItemSubcategories.Drinks,
                new[]
                {
                    new ItemInfo("Canteen", Rarities.Rare),
                    new ItemInfo("Waterbottle", Rarities.Rare),
                    new ItemInfo("Soda Can", Rarities.Common)
                });

            #endregion

            #region Equipment

            // http://dayz.gamepedia.com/Equipment

            const ItemCategories equipment = ItemCategories.Equipment;

            // Backpacks
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Backpacks,
                new[]
                {
                    new ItemInfo("Smersh Backpack", Rarities.Rare),
                    new ItemInfo("Child Briefcase", Rarities.Rare),
                    new ItemInfo("Improvised Courier Bag", Rarities.NA),
                    new ItemInfo("Improvised Backpack", Rarities.NA),
                    new ItemInfo("Leather Sack", Rarities.NA),
                    new ItemInfo("Leather Courier Bag", Rarities.NA),
                    new ItemInfo("Improvised Leather Backpack", Rarities.NA),
                    new ItemInfo("Taloon Backpack", Rarities.Uncommon),
                    new ItemInfo("Hunting Backpack", Rarities.Rare),
                    new ItemInfo("Mountain Backpack", Rarities.Rare)
                });


            // Containers
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Containers,
                new[]
                {
                    new ItemInfo("Ammo Box", Rarities.Rare),
                    new ItemInfo("Small Protector Case", Rarities.Rare),
                    new ItemInfo("First Aid Kit", Rarities.Uncommon)
                });


            // Cooking
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Cooking,
                new[]
                {
                    new ItemInfo("Cooking Pot", Rarities.Rare),
                    new ItemInfo("Fireplace Kit", Rarities.NA),
                    new ItemInfo("Frying Pan", Rarities.Rare),
                    new ItemInfo("Kitchen Knife", Rarities.Rare),
                    new ItemInfo("Portable Gas Stove", Rarities.Rare)
                });


            // Decorational
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Decorational,
                new[]
                {
                    new ItemInfo("Cow Pelt", Rarities.NA),
                    new ItemInfo("Deer Pelt", Rarities.NA),
                    new ItemInfo("Pig Pelt", Rarities.Unknown),
                    new ItemInfo("Wild Boar Pelt", Rarities.NA),
                    new ItemInfo("Rabbit Pelt", Rarities.NA)
                });


            // Devices
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Devices,
                new[]
                {
                    new ItemInfo("Alkaline Battery 9V", Rarities.Rare),
                    new ItemInfo("Binoculars", Rarities.Rare),
                    new ItemInfo("Compass", Rarities.Uncommon),
                    new ItemInfo("WalkieTalkie", Rarities.Uncommon)
                });


            // LightSources
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.LightSources,
                new[]
                {
                    new ItemInfo("Chemlight", Rarities.Common),
                    new ItemInfo("Flashlight", Rarities.Rare),
                    new ItemInfo("Headtorch", Rarities.Uncommon),
                    new ItemInfo("Portable Gas Lamp", Rarities.Rare),
                    new ItemInfo("Road Flare", Rarities.Common)
                });


            // Repair Kits
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.RepairKits,
                new[]
                {
                    new ItemInfo("Sewing Kit", Rarities.Rare),
                    new ItemInfo("Weapon Cleaning Kit", Rarities.Rare)
                });


            // Resources
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Resources,
                new[]
                {
                    new ItemInfo("Ashwood Stick", Rarities.VeryRare),
                    new ItemInfo("Chicken Feather", Rarities.NA),
                    new ItemInfo("Firewood", Rarities.Uncommon),
                    new ItemInfo("Stone", Rarities.Rare),
                    new ItemInfo("Wooden Stick", Rarities.Rare)
                });

            
            // Tents
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Tents,
                new[]
                {
                    new ItemInfo("Tent", Rarities.Rare)
                });


            // Tools
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Tools,
                new[]
                {
                    new ItemInfo("Farming Hoe", Rarities.Uncommon),
                    new ItemInfo("Firefighter Axe", Rarities.Rare),
                    new ItemInfo("Pickaxe", Rarities.Rare),
                    new ItemInfo("Shovel", Rarities.Common),
                    new ItemInfo("Splitting Axe", Rarities.Rare),
                    new ItemInfo("Can Opener", Rarities.Rare),
                    new ItemInfo("Hammer", Rarities.Rare),
                    new ItemInfo("Matchbox", Rarities.Rare),
                    new ItemInfo("Pliers", Rarities.Rare),
                    new ItemInfo("Screwdriver", Rarities.Rare),
                    new ItemInfo("Wrench", Rarities.Rare)
                });


            // Other
            //-------------------
            CreateItem(context, equipment, ItemSubcategories.Other,
                new[]
                {
                    new ItemInfo("Burlap Sack", Rarities.Uncommon),
                    new ItemInfo("Duct Tape", Rarities.Rare),
                    new ItemInfo("Fishing Bait", Rarities.NA),
                    new ItemInfo("Fishing Hook", Rarities.Rare),
                    new ItemInfo("Gas Canister", Rarities.Rare),
                    new ItemInfo("Handcuffs", Rarities.Rare),
                    new ItemInfo("Handcuff Keys", Rarities.Rare),
                    new ItemInfo("Map", Rarities.Common),
                    new ItemInfo("Paper", Rarities.Uncommon),
                    new ItemInfo("Pen", Rarities.Uncommon),
                    new ItemInfo("Rope", Rarities.Uncommon),
                    new ItemInfo("Sharpened Stick", Rarities.NA),
                    new ItemInfo("Spraypaint", Rarities.Common)
                });

            #endregion

            #region Clothing

            // http://dayz.gamepedia.com/Clothing

            const ItemCategories clothing = ItemCategories.Clothing;

            // Eyewear
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Eyewear,
                new[]
                {
                    new ItemInfo("Designer Sunglasses", Rarities.Uncommon),
                    new ItemInfo("Glasses with thin frames", Rarities.Uncommon),
                    new ItemInfo("Glasses with thick frames", Rarities.Uncommon),
                    new ItemInfo("Rocket Aviators", Rarities.Uncommon)
                });


            // Hats
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Hats,
                new[]
                {
                    new ItemInfo("Bandana", Rarities.Uncommon),
                    new ItemInfo("Baseball Cap", Rarities.Common),
                    new ItemInfo("Beanie Hat", Rarities.Common),
                    new ItemInfo("Beret", Rarities.Uncommon),
                    new ItemInfo("Boonie Hat", Rarities.Common),
                    new ItemInfo("Cowboy Hat", Rarities.Uncommon),
                    new ItemInfo("Flat Cap", Rarities.Common),
                    new ItemInfo("Pilotka", Rarities.Rare),
                    new ItemInfo("Police Cap", Rarities.Uncommon),
                    new ItemInfo("Radar Cap", Rarities.Common),
                    new ItemInfo("Soviet Army Officer's Hat", Rarities.Rare),
                    new ItemInfo("Ushanka", Rarities.Uncommon),
                    new ItemInfo("Zmijovka Cap", Rarities.Common)
                });


            // Helmets
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Helmets,
                new[]
                {
                    new ItemInfo("Ballistic Helmet", Rarities.Rare),
                    new ItemInfo("Gorka E Military Helmet", Rarities.Rare),
                    new ItemInfo("Hard Hat", Rarities.Uncommon),
                    new ItemInfo("Motorbike Helmet", Rarities.Common),
                    new ItemInfo("Tanker Helmet", Rarities.Rare),
                    new ItemInfo("ZSh3 Pilot Helmet", Rarities.Rare)
                });


            // Masks
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Masks,
                new[]
                {
                    new ItemInfo("Dallas mask", Rarities.Uncommon),
                    new ItemInfo("Hotxon mask", Rarities.Uncommon),
                    new ItemInfo("Wolf mask", Rarities.Uncommon),
                    new ItemInfo("Balaclava", Rarities.Rare),
                    new ItemInfo("Gas mask", Rarities.Rare),
                    new ItemInfo("Respirator", Rarities.Rare)
                });


            // Hands and Arms
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.HandsAndArms,
                new[]
                {
                    new ItemInfo("Working Gloves", Rarities.Uncommon)
                });


            // Shirts and Jackets
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.ShirtsAndJackets,
                new[]
                {
                    new ItemInfo("Down Jacket", Rarities.Uncommon),
                    new ItemInfo("Firefighter Jacket", Rarities.Rare),
                    new ItemInfo("Gorka Military Uniform Jacket", Rarities.Rare),
                    new ItemInfo("Hoodie", Rarities.Uncommon),
                    new ItemInfo("OREL Unit Uniform Jacket", Rarities.Rare),
                    new ItemInfo("Paramedic Jacket", Rarities.Rare),
                    new ItemInfo("Police Uniform Jacket", Rarities.Uncommon),
                    new ItemInfo("Raincoat", Rarities.Uncommon),
                    new ItemInfo("Riders Jacket", Rarities.Rare),
                    new ItemInfo("Shirt", Rarities.Common),
                    new ItemInfo("T-Shirt", Rarities.Common),
                    new ItemInfo("Tactical Shirt", Rarities.Rare),
                    new ItemInfo("Tracksuit Jacket", Rarities.Uncommon),
                    new ItemInfo("TTsKO Jacket", Rarities.Rare),
                    new ItemInfo("Wool Coat", Rarities.Common)
                });


            // Holsters
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Holsters,
                new[]
                {
                    new ItemInfo("Chest Holster", Rarities.Uncommon)
                });


            // Vests
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Vests,
                new[]
                {
                    new ItemInfo("Anti-stab Vest", Rarities.VeryRare),
                    new ItemInfo("Blue Press Vest", Rarities.VeryRare),
                    new ItemInfo("AHigh Capacity Vest", Rarities.Rare),
                    new ItemInfo("Smersh Vest", Rarities.VeryRare),
                    new ItemInfo("Smersh Vest with Backpack attached", Rarities.NA),
                    new ItemInfo("UK Assault Vest", Rarities.Rare)
                });


            // Pants
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Pants,
                new[]
                {
                    new ItemInfo("Chernarus Police Uniform Pants", Rarities.Uncommon),
                    new ItemInfo("Canvas Pants", Rarities.Common),
                    new ItemInfo("Canvas Pants Short", Rarities.Common),
                    new ItemInfo("Cargo Pants", Rarities.Uncommon),
                    new ItemInfo("Gorka Military Pants", Rarities.Rare),
                    new ItemInfo("Hunter Pants", Rarities.Uncommon),
                    new ItemInfo("Jeans", Rarities.Common),
                    new ItemInfo("Paramedic Pants", Rarities.Uncommon),
                    new ItemInfo("OREL Unit Uniform Pants", Rarities.Rare),
                    new ItemInfo("Tracksuit Pants", Rarities.Common),
                    new ItemInfo("TTsKO Pants", Rarities.Uncommon)
                });


            // Boots
            //-------------------
            CreateItem(context, clothing, ItemSubcategories.Boots,
                new[]
                {
                    new ItemInfo("Athletic Shoes", Rarities.Common),
                    new ItemInfo("Combat Boots", Rarities.Rare),
                    new ItemInfo("Hiking Boots", Rarities.Uncommon),
                    new ItemInfo("Jogging Shoes", Rarities.Uncommon),
                    new ItemInfo("Jungle Boots", Rarities.Rare),
                    new ItemInfo("Leather Shoes", Rarities.Uncommon),
                    new ItemInfo("Low Hiking Boots", Rarities.Uncommon),
                    new ItemInfo("Military Boots", Rarities.Rare),
                    new ItemInfo("Wellies", Rarities.Uncommon),
                    new ItemInfo("Working Boots", Rarities.Uncommon)
                });

            #endregion

            #endregion
        }

        #region Helpers

        private static void CreateRoles(ApplicationDbContext context)
        {
            DefineRole(context, AdministratorRole);

            context.SaveChanges();
        }

        private static void CreateUsers(ApplicationDbContext context)
        {
            var mgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            foreach (var user in DefaultUsers.All)
            {
                DefineUser(mgr, user);
            }
        }

        private static void DefineRole(ApplicationDbContext context, string role)
        {
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole(role));
        }

        private static void DefineUser(ApplicationUserManager mgr, User user)
        {
            var email = user.UserName.ToLower() + "@mail.com";

            var applicationUser = new ApplicationUser
            {
                Id = user.UserId,
                UserName = user.UserName,
                Email = email,
                EmailConfirmed = true
            };
            var result = mgr.Create(applicationUser, "qwerty");

            if (result.Succeeded)
            {
                if (user.IsAdmin)
                {
                    mgr.AddToRole(applicationUser.Id, AdministratorRole);
                }

                mgr.AddLogin(
                    applicationUser.Id,
                    new UserLoginInfo("Steam", "http://steamcommunity.com/openid/id/" + user.SteamId));
            }
        }

        private static void CreateItem(
            ApplicationDbContext context,
            ItemCategories category,
            ItemSubcategories subcategory,
            IEnumerable<ItemInfo> itemInfos)
        {
            var details = new CategoryInfo
            {
                Category = category,
                Subcategory = subcategory
            };

            context.Items.AddOrUpdate(
                item => item.Name,
                itemInfos.Select(
                    info => new Item(info.Name, details, info.Rariry)).ToArray());
        }

        #endregion

        private const string AdministratorRole = "Administrator";
    }

    internal class ItemInfo
    {
        public ItemInfo(string name, Rarities rarity)
        {
            Rariry = rarity;
            Name = name;
        }

        public Rarities Rariry { get; private set; }
        public string Name { get; set; }
    }
}
