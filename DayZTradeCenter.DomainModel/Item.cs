using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
{
    public class Item : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}