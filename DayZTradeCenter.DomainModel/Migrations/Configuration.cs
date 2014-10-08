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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Uncomment to debug code-first migrations code
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            CreateRoles(context);
            CreateUsers(context);

            #region Weapons

            // http://dayz.gamepedia.com/Weapons
            const ItemCategories weapons = ItemCategories.Weapons;

            // Melee Weapons
            //-------------------

            //  Blunt weapons
            CreateItem(context, weapons, ItemSubcategories.BluntMelee,
                new[]
                {
                    "Baseball bat",
                    "Crowbar",
                    "Fire Extinguisher",
                    "Pipe Wrench",
                    "Telescopic Baton"
                });

            //  Bladed weapons
            CreateItem(context, weapons, ItemSubcategories.BladedMelee,
                new[]
                {
                    "Combat Knife",
                    "Machete",
                    "Sickle",
                    "Pitchfork"
                });

            //  Electroshock Weapons
            CreateItem(context, weapons, ItemSubcategories.ElectricWeapons,
                new[]
                {
                    "Electric Cattle Prod",
                    "Stun Baton"
                });


            // Bows
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Bows,
                new[]
                {
                    "Crossbow",
                    "Improvised Ashwood Short Bow"
                });

            
            // Handguns
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Handguns,
                new[]
                {
                    "Amphibia S",
                    "Makarov IJ70",
                    "P1",
                    "CR75",
                    "FNX45",
                    "1911",
                    "1911 Engraved",
                    "Magnum",
                    "LongHorn"
                });


            // Rifles
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Rifles,
                new[]
                {
                    "Sporter 22",
                    "AK101",
                    "M4A1",
                    "CR527 Carbine",
                    "SKS",
                    "AKM",
                    "Blaze 95 Double Rifle",
                    "Mosin 9130",
                    "Sawed-off Mosin 9130"
                });


            // Submachine - Guns
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.SubmachineGuns,
                new[]
                {
                    "PM73 RAK",
                    "MP5-K"
                });


            // Shotguns
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.SubmachineGuns,
                new[]
                {
                    "IZH-43",
                    "Sawed-off IZH-43"
                });


            // Grenades
            //-------------------
            CreateItem(context, weapons, ItemSubcategories.Grenades,
                new[]
                {
                    "Explosive Grenade",
                    "Flashbang",
                    "RDG-5 Explosive Grenade"
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
                    "Bandage",
                    "Rags"
                });


            // Splints
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Splints,
                new[]
                {
                    "Wooden Splint"
                });


            // Blood Transfusion
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.BloodTransfusion,
                new[]
                {
                    "Blood Bag",
                    "Blood Bag Kit",
                    "Saline Bag",
                    "Saline Bag IV",
                    "IV Start Kit"
                });


            // Blood Testing
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.BloodTesting,
                new[]
                {
                    "Blood Testing Kit",
                    "Syringe"
                });


            // Disinfection
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Disinfection,
                new[]
                {
                    "Alcohol Tincture",
                    "Disinfectant Spray",
                    "Water Purification Tablets"
                });


            // Medicines
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Medicines,
                new[]
                {
                    "Charcoal Tabs",
                    "Injection Vial",
                    "Morphine Auto-Injector",
                    "Painkillers",
                    "Tetracycline Antibiotics",
                    "Vitamin Bottles"
                });


            // Reanimation
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Reanimation,
                new[]
                {
                    "Defibrillator",
                    "Epinephrine"
                });


            // Temperature
            //-------------------
            CreateItem(context, medicalSupplies, ItemSubcategories.Temperature,
                new[]
                {
                    "Medical Thermometer"
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
                    "AK Folding Buttstock",
                    "AK Plastic Buttstock",
                    "AK Wooden Buttstock",
                    "M4 Buttstock CQB",
                    "M4 Buttstock OE",
                    "M4 Buttstock MP",
                    "MP5 OEM Buttstock"
                });


            // Handguards
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Handguards,
                new[]
                {
                    "AK Handguard Rail",
                    "AK Handguard Plastic",
                    "AK Wooden Handguard",
                    "M4 Handguard MP",
                    "M4 Handguard Plastic",
                    "M4 Handguard RIS",
                    "MP5 Plastic Handguard"
                });


            // Sights and Optics
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.SightsAndOptics,
                new[]
                {
                    "ACOG Optics",
                    "BUIS",
                    "Crossbow Holosight",
                    "FNP45 MRD",
                    "Long Range Scope",
                    "M4 Carryhandle Optics",
                    "M68 CompM2 Optics",
                    "PSO1 Scope",
                    "PU Scope",
                    "RV1 RDS Optics",
                    "Crossbow Scope"
                });


            // Illumination
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Illumination,
                new[]
                {
                    "Pistol Flashlight",
                    "Weapon Flashlight"
                });


            // Muzzle Attachments
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.MuzzleAttachments,
                new[]
                {
                    "Mosin M44 Compensator",
                    "Pistol Suppressor",
                    "Suppressor 556",
                    "Suppressor 5.45x39mm"
                });


            // Bayonets
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Bayonets,
                new[]
                {
                    "M9A1 Bayonet",
                    "M91 Bayonet",
                    "SKS Bayonet"
                });


            // Bipods
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Bipods,
                new[]
                {
                    "ATLAS Bipod"
                });


            // Wraps
            //-------------------
            CreateItem(context, attachments, ItemSubcategories.Wraps,
                new[]
                {
                    "Burlap Wrap",
                    "Grass Wrap"
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
                    ".22 Round(s)",
                    ".380 Auto Round(s)",
                    "9mm Round(s)",
                    ".45ACP Round(s)",
                    ".357 Round(s)"
                });


            // Shotgun Ammunition
            //-------------------
            CreateItem(context, ammunition, ItemSubcategories.ShotgunAmmunition,
                new[]
                {
                    "12 Gauge Buckshot"
                });


            // Rifle Ammunition
            //-------------------
            CreateItem(context, ammunition, ItemSubcategories.RifleAmmunition,
                new[]
                {
                    "7.62x39mm Round(s)",
                    "5.56mm Round(s)",
                    "7.62mm Round(s)"
                });


            // Miscellaneous Ammunition
            //-------------------
            CreateItem(context, ammunition, ItemSubcategories.MiscellaneousAmmunition,
                new[]
                {
                    "Crossbow bolt(s)",
                    "Composite Arrow",
                    "Improvised Arrow"
                });

            #endregion

            #region Magazines

            #endregion

            #region Food

            #endregion

            #region Drink

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
            IEnumerable<string> itemNames)
        {
            var details = new CategoryInfo
            {
                Category = category,
                Subcategory = subcategory
            };

            context.Items.AddOrUpdate(
                item => item.Name,
                itemNames.Select(
                    name => new Item(name, details)).ToArray());
        }

        #endregion

        private const string AdministratorRole = "Administrator";
    }
}
