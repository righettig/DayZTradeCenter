namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DayZTradeCenterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DayZTradeCenterDbContext context)
        {
            context.Items.AddOrUpdate(item => item.Name,

                // http://dayz.gamepedia.com/Weapons

                // Melee Weapons
                //-------------------
                //  Blunt weapons
                new Item {Name = "Baseball bat"},
                new Item {Name = "Crowbar"},
                new Item {Name = "Fire Extinguisher"},
                new Item {Name = "Pipe Wrench"},
                new Item {Name = "Telescopic Baton"},

                //  Bladed weapons
                new Item {Name = "Combat Knife"},
                new Item {Name = "Machete"},
                new Item {Name = "Sickle"},
                new Item {Name = "Pitchfork"},

                //  Electroshock Weapons
                new Item {Name = "Electric Cattle Prod"},
                new Item {Name = "Stun Baton"},


                // Bows
                //-------------------
                new Item {Name = "Crossbow"},
                new Item {Name = "Improvised Ashwood Short Bow"},


                // Handguns
                //-------------------
                new Item {Name = "Amphibia S"},
                new Item {Name = "Makarov IJ70"},
                new Item {Name = "P1"},
                new Item {Name = "CR75"},
                new Item {Name = "FNX45"},
                new Item {Name = "1911"},
                new Item {Name = "1911 Engraved"},
                new Item {Name = "Magnum"},
                new Item {Name = "LongHorn"},


                // Rifles
                //-------------------
                new Item {Name = "Sporter 22"},
                new Item {Name = "AK101"},
                new Item {Name = "M4A1"},
                new Item {Name = "CR527 Carbine"},
                new Item {Name = "SKS"},
                new Item {Name = "AKM"},
                new Item {Name = "Blaze 95 Double Rifle"},
                new Item {Name = "Mosin 9130"},
                new Item {Name = "Sawed-off Mosin 9130"},


                // Submachine - Guns
                //-------------------
                new Item {Name = "PM73 RAK"},
                new Item {Name = "MP5-K"},


                // Shotguns
                //-------------------
                new Item {Name = "IZH-43"},
                new Item {Name = "Sawed-off IZH-43"},


                // Grenades
                //-------------------
                new Item {Name = "Explosive Grenade"},
                new Item {Name = "Flashbang"},
                new Item {Name = "RDG-5 Explosive Grenade"}
            );
        }
    }
}
