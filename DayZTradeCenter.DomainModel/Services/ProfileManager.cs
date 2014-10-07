﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DayZTradeCenter.DomainModel.Interfaces;
using rg.GenericRepository.Core;
using rg.Time;

namespace DayZTradeCenter.DomainModel.Services
{
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

        /// <summary>
        /// Adds the specified event to the history of the user with the given user id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="event">The event.</param>
        public void AddHistoryEvent(string userId, Events @event)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(userId), "'userId' is null, empty or made of only whitespaces.");

            _eventsRepository.Insert(new EventInfo(userId, @event));
            _eventsRepository.SaveChanges();
        }

        /// <summary>
        /// Gets the history events of the user with the specified user id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The history events listed in reverse chronological order.
        /// </returns>
        public IEnumerable<EventInfo> GetHistoryByUserId(string userId)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(userId), "'userId' is null, empty or made of only whitespaces.");

            return _eventsRepository
                .GetAll()
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.TimeStamp);
        }

        private readonly IRepository<EventInfo> _eventsRepository;
    }

    public class EventInfo : IEntity
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventInfo"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="event">The event.</param>
        /// <exception cref="System.ArgumentException">
        /// 'userId' cannot be null, empty of made of only whitespaces.
        /// </exception>
        public EventInfo(string userId, Events @event)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("'userId' cannot be null, empty of made of only whitespaces.");
            }

            UserId = userId;
            TimeStamp = TimeProvider.Now;
            Event = @event;
        }

        /// <summary>
        /// Required by EF.
        /// </summary>
        private EventInfo()
        {
        }

        #endregion
        
        public int Id { get; set; }
        public string UserId { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public Events Event { get; private set; }
    }

    public enum Events
    {
        Registration,
        ProfileUpdate,
        TradeCreated,
        TradeDeleted,
        TradeOffered,
        TradeWithdrawn,
        WinnerChoosen,
        TradeCompleted,
        FeedbackLeft
    }
}