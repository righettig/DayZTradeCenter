using System;
using System.Collections.Generic;
using System.Linq;
using rg.GenericRepository.Core;
using rg.Time;

namespace DayZTradeCenter.DomainModel
{
    public enum Events
    {
        Registration
    }

    public class EventInfo : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Events Event { get; set; }
    }

    public interface IProfileManager
    {
        void AddHistoryEvent(string userId, Events @event);
        IEnumerable<EventInfo> GetHistoryByUserId(string userId);
    }

    public class ProfileManager : IProfileManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileManager"/> class.
        /// </summary>
        /// <param name="eventsRepository">The events repository.</param>
        /// <exception cref="System.ArgumentNullException">eventsRepository</exception>
        public ProfileManager(IRepository<EventInfo> eventsRepository)
        {
            if (eventsRepository == null)
            {
                throw new ArgumentNullException("eventsRepository");
            }

            _eventsRepository = eventsRepository;
        }

        public void AddHistoryEvent(string userId, Events @event)
        {
            _eventsRepository.Insert(new EventInfo
            {
                UserId = userId, 
                TimeStamp = TimeProvider.Now,
                Event = @event
            });
            _eventsRepository.SaveChanges();
        }

        public IEnumerable<EventInfo> GetHistoryByUserId(string userId)
        {
            return _eventsRepository
                .GetAll()
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.TimeStamp);
        }

        private readonly IRepository<EventInfo> _eventsRepository;
    }
}
