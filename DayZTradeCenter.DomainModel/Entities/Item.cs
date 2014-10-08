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

        public Item()
        {
        }

        #endregion
        
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryInfo Details { get; set; }
    }

    public class CategoryInfo
    {
        public ItemCategories Category { get; set; }
        public ItemSubcategories Subcategory { get; set; }
    }

    public enum ItemCategories
    {
        Weapons,
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
        Grenades
    }
}
