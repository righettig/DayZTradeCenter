using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Services;

namespace DayZTradeCenter.DomainModel.Interfaces
{
    public interface IProfileManager
    {
        /// <summary>
        /// Adds the specified event to the history of the user with the given user id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="event">The event.</param>
        void AddHistoryEvent(string userId, Events @event);

        /// <summary>
        /// Gets the history events of the user with the specified user id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The history events listed in reverse chronological order.</returns>
        IEnumerable<EventInfo> GetHistoryByUserId(string userId);
    }
}