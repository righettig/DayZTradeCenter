using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Entities;

namespace DayZTradeCenter.UI.Web.Models
{
    public class ExchangeManagementViewModel
    {
        public ExchangeManagementViewModel()
        {
            Messages = new List<Message>();
        }

        public Trade Trade { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ExchangeDetails Details { get; set; }
    }

    public class ExchangeDetails
    {
        public string SteamId { get; set; }
        public string Location { get; set; }
        public string Server { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}