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
