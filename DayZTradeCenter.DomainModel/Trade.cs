using System;
using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Identity.Entities;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
{
    public class Trade : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trade"/> class.
        /// </summary>
        public Trade()
        {
            Have = new List<Item>();
            Want = new List<Item>();
            Offers = new List<IApplicationUser>();
        }

        #region Public properties

        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Item> Have { get; private set; }
        public ICollection<Item> Want { get; private set; }
        public ICollection<IApplicationUser> Offers { get; private set; }
        public IApplicationUser Owner { get; set; }

        #endregion
    }
}
