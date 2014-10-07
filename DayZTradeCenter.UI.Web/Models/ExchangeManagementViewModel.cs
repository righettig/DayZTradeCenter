using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Entities.Messages;

namespace DayZTradeCenter.UI.Web.Models
{
    public class ExchangeManagementViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeManagementViewModel"/> class.
        /// </summary>
        public ExchangeManagementViewModel()
        {
            Messages = new List<Message>();
        }

        public Trade Trade { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ExchangeDetails Details { get; set; }
    }
}