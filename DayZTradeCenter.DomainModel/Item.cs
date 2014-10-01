using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
{
    /// <summary>
    /// A DayZ game item.
    /// </summary>
    public class Item : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}